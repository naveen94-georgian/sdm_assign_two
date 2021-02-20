using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SocialDataMiningApp.Models;
using SocialDataMiningApp.Services;
using System;
using System.Net;
using System.Threading.Tasks;
using TweetSharp;

namespace SocialDataMiningApp.Controllers
{
    public class TwitterController : Controller
    {
        private string oAuthConsumerKey = String.Empty;
        private string oAuthConsumerSecret = String.Empty;
        private string accessToken = String.Empty;
        private string accessTokenSecret = String.Empty;

        public IConfiguration Configuration { get; }
        private readonly TwitterModelService _tmService;

        public TwitterController(IConfiguration configuration, TwitterModelService tmService)
        {
            Configuration = configuration;
            oAuthConsumerKey = Configuration["Authentication:Twitter:ConsumerAPIKey"];
            oAuthConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
            accessToken = Configuration["Authentication:Twitter:ConsumerAccessToken"];
            accessTokenSecret = Configuration["Authentication:Twitter:ConsumerAccessSecret"];
            _tmService = tmService;
        }

        [HttpPost]
        public IActionResult Send([Bind("text")] TwitterModel tweetModel)
        {
            TwitterService service = new TwitterService(oAuthConsumerKey, oAuthConsumerSecret, accessToken, accessTokenSecret);
            var res = service.SendTweet(new SendTweetOptions { Status = tweetModel.text }, async (tweet, response) =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    tweetModel.idStr = tweet.IdStr;
                    tweetModel.author = tweet.Author.ScreenName;
                    tweetModel.retweetCount = tweet.RetweetCount;
                    tweetModel.CreationDate = tweet.CreatedDate;
                    tweetModel.language = tweet.Language;
                    Console.WriteLine(tweet.Location);
                    await _tmService.Create(tweetModel);
                }
            });
            res.AsyncWaitHandle.WaitOne();
            return View("Confirm");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
