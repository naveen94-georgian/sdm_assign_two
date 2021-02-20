using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Threading.Tasks;
using SocialDataMiningApp.Models;

namespace SocialDataMiningApp.Services
{
    public class TwitterModelService
    {
        private readonly IMongoCollection<TwitterModel> _tweets;
        public TwitterModelService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDbConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDb:Database"]);
            _tweets = database.GetCollection<TwitterModel>("sdm_twitter_col");
        }

        public async Task<TwitterModel> Create(TwitterModel tweet)
        {
            await _tweets.InsertOneAsync(tweet);
            return tweet;
        }
    }
}
