using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLayer
{
    public class BookModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string bookID { get; set; }
        public string bookname { get; set; }

        public string authorname { get; set; }

        public decimal rating { get; set; }

        public int totalRating { get; set; }

        public string bookprice { get; set; }

        public string bookimage { get; set; }

        public string bookdescription { get; set; }

        public int bookquantity { get; set; }

        [ForeignKey("AdminModel")]
        public string adminID { get; set; }
        //public virtual AdminRegisterModel AdminRegister { get; set; }

    }
}
