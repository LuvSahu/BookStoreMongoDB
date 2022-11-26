using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Repository
{
    public class AdminRL : IAdminRL
    {
      

        private readonly IMongoCollection<AdminRegisterModel> Admin;


        private readonly IConfiguration configuration;

        public AdminRL(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var userlimit = new MongoClient(db.ConnectionString);
            var database = userlimit.GetDatabase(db.DatabaseName);
            
            Admin = database.GetCollection<AdminRegisterModel>("Admin");
        }

        public AdminRegisterModel Register(AdminRegisterModel register)
        {
            try
            {
                var check = this.Admin.AsQueryable().Where(x => x.adminemailID == register.adminemailID).SingleOrDefault();
                if (check == null)
                {
                    this.Admin.InsertOne(register);
                    return register;
                }
                return null;
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public string Login(AdminLoginModel login)
        {
            try
            {
                var check = this.Admin.AsQueryable().Where(x => x.adminemailID == login.adminemailID).SingleOrDefault();
                if (check != null)
                {
                    check = this.Admin.AsQueryable().Where(x => x.password == login.password).SingleOrDefault();
                    if (check != null)
                    {
                        
                        //return check;

                        var token = GenerateSecurityToken(check.adminemailID, check.adminID);
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

        public string GenerateSecurityToken(string email, string AdminId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration[("JWT:key")]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("AdminId", AdminId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
