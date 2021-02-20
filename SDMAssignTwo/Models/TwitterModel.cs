using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SocialDataMiningApp.Models
{
    public class TwitterModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string idStr { get; set; }
        public string text { get; set; }
        public string author { get; set; }
        public int retweetCount { get; set; }
        public string language { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
