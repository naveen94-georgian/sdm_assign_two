using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialDataMiningApp.Models
{
    public class RedditModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string post_id { get; set; }
        public string title { get; set; }
        public string post { get; set; }
        public string name { get; set; }
        public int upvotes { get; set; }
        public string subreddit { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
