using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer
{
    public class AdminRegisterModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string adminID { get; set; }
        public string adminfullname { get; set; }

        public string adminemailID { get; set; }

        public string password { get; set; }

        public string adminmobile { get; set; }
    }
}
