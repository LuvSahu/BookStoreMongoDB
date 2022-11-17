using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelLayer
{
    public class CartModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string cartID { get; set; }

        [ForeignKey("RegisterModel")]
        public string userID { get; set; }
       // public virtual RegisterModel RegisterModel { get; set; }

        [ForeignKey("BooksModel")]
        public string bookID { get; set; }
       // public virtual BookModel BooksModel { get; set; }
        public int quantity { get; set; }
    }
}
