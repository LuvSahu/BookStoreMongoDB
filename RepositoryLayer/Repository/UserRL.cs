using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class UserRL : IUserRL
    {
        private readonly IMongoCollection<RegisterModel> User;

        
        private readonly IConfiguration configuration;

        public UserRL(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var userlimit = new MongoClient(db.ConnectionString);
            var database = userlimit.GetDatabase(db.DatabaseName);
            User = database.GetCollection<RegisterModel>("User");
            
        }

        public RegisterModel Register(RegisterModel register)
        {
            try
            {
                var check = this.User.AsQueryable().Where(x => x.emailID == register.emailID).SingleOrDefault();
                if (check == null)
                {
                    this.User.InsertOne(register);
                    return register;
                }
                return null;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public string Login(LoginModel login)
        {
            try
            {
                var check = this.User.AsQueryable().Where(x => x.emailID == login.emailID).SingleOrDefault();
                if (check != null)
                {
                    check = this.User.AsQueryable().Where(x => x.password == login.password).SingleOrDefault();
                    if (check != null)
                    {
                        //ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect();
                        //IDatabase database = connectionMultiplexer.GetDatabase();
                        //database.StringSet(key: "FullName", check.fullname);
                        //database.StringSet(key: "Email", check.emailID);
                        //database.StringSet(key: "Mobile", check.mobile);
                        //database.StringSet(key: "UserId", check.userID);

                        //return check;

                        var token = GenerateSecurityToken(check.emailID,check.userID);
                        return token;

                    }
                    return null;
                }

                return null;
            }

            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GenerateSecurityToken(string email, string UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration[("JWT:key")]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public string FogotPassword(string Email)
        {
            try
            {
                var EmailCheck = this.User.AsQueryable().Where(x => x.emailID == Email).SingleOrDefault();
                if (EmailCheck != null)
                {

                    string token = GenerateSecurityToken(EmailCheck.emailID, EmailCheck.userID);
                    MSMQModel mSMQModel = new MSMQModel();
                    mSMQModel.sendData2Queue(token);
                    return "Mail sent";
                }
                else
                {
                    return null;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ResetLink(string Email, string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    var EmailCheck = this.User.AsQueryable().Where(x => x.emailID == Email).SingleOrDefault();
                    EmailCheck.password = password;

                    //User.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
