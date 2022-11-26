using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
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
    public class BookRL : IBookRL
    {
        private readonly IMongoCollection<BookModel> Books;

        private readonly IConfiguration configuration;

        private readonly IConfiguration cloudinaryEntity;


        public BookRL(IDBSetting db, IConfiguration configuration, IConfiguration cloudinaryEntity)
        {
            this.configuration = configuration;
            this.cloudinaryEntity = cloudinaryEntity;
            var userlimit = new MongoClient(db.ConnectionString);
            var database = userlimit.GetDatabase(db.DatabaseName);
            Books = database.GetCollection<BookModel>("Books");
        }

        public BookModel AddBook(BookModel addbook,string adminid)
        {
            try
            {
                var ifExists = this.Books.Find(x => x.bookID == addbook.bookID && x.adminID == adminid).SingleOrDefault();
                if (ifExists == null)
                {
                    this.Books.InsertOne(addbook);
                    return addbook;
                }
                return null;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        public BookModel UpdateBook(BookModel editbook,string id,string adminid)
        {
            try
            {
                var ifExists = this.Books.Find(x => x.bookID == id && x.adminID == adminid).FirstOrDefault();
                if (ifExists != null)
                {
                    
                    this.Books.UpdateOne(x => x.bookID == id,Builders<BookModel>.Update.Set(x => x.bookname, editbook.bookname)
                        .Set(x => x.bookdescription, editbook.bookdescription)
                        .Set(x => x.authorname, editbook.authorname)
                        .Set(x => x.rating, editbook.rating)
                        .Set(x => x.totalRating, editbook.totalRating)
                        .Set(x => x.bookprice, editbook.bookprice)
                        .Set(x => x.bookimage, editbook.bookimage)
                        .Set(x => x.bookquantity, editbook.bookquantity));
                    return ifExists;
                }
                else
                {
                    this.Books.InsertOne(editbook);
                    return editbook;
                }
            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteBook(string id)
        {
            try
            {
                var ifExists = this.Books.FindOneAndDelete(x => x.bookID == id);
                return true;

            }
            catch (ArgumentNullException e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<BookModel> GetAllBook()
        {
            return Books.Find(FilterDefinition<BookModel>.Empty).ToList();
        }

        public BookModel GetByBookId(string id)
        {
            return Books.Find(x => x.bookID == id).FirstOrDefault();
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

        public string UploadImage(string id, IFormFile img, string adminid)
        {
            try
            {
                var result = this.Books.Find(x => x.bookID == id && x.adminID == adminid).SingleOrDefault();
                if (result != null)
                {
                    Account cloudaccount = new Account(
                         cloudinaryEntity["CloudinarySettings:cloudName"],
                         cloudinaryEntity["CloudinarySettings:apiKey"],
                         cloudinaryEntity["CloudinarySettings:apiSecret"]
                         );

                    Cloudinary cloudinary = new Cloudinary(cloudaccount);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, img.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadResult.Url.ToString();
                    result.bookimage = imagePath;
                    this.Books.UpdateOne(x => x.bookID == id, Builders<BookModel>.Update.Set(x => x.bookimage, imagePath));
                    return "Image upload SuccessFully";
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
