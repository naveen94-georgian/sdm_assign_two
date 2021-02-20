using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SocialDataMiningApp.Models;

namespace SocialDataMiningApp.Services
{
    public class RedditService
    {
        private readonly IMongoCollection<RedditModel> _posts;

        public RedditService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDbConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDb:Database"]);
            _posts = database.GetCollection<RedditModel>("sdm_reddit_col");
        }

        public async Task<RedditModel> Create(RedditModel post)
        {
            await _posts.InsertOneAsync(post);
            return post;
        }
    }
}
