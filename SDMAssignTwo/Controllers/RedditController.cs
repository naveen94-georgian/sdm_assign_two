using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Reddit;
using System;
using System.Threading.Tasks;
using SocialDataMiningApp.Models;
using SocialDataMiningApp.Services;

namespace SocialDataMiningApp.Controllers
{

    public class RedditController : Controller
    {
        private string appId;
        private string refreshToken;
        private string appSecret;
        private string accessToken;
        private readonly RedditService _redditService;

        public RedditController(IConfiguration configuration, RedditService redditService)
        {
            appId = configuration["Authentication:Reddit:appId"];
            refreshToken = configuration["Authentication:Reddit:refreshToken"];
            appSecret = configuration["Authentication:Reddit:appSecret"];
            accessToken = configuration["Authentication:Reddit:accessToken"];
            _redditService = redditService;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send([Bind("title,post")] RedditModel redditModel)
        {
            var reddit = new RedditClient(appId, refreshToken, appSecret, accessToken, "v1");
            var mySub = reddit.Subreddit("sircmpwn");
            var mySelfPost = mySub.SelfPost(redditModel.title, redditModel.post);


            var post = await mySelfPost.SubmitAsync();
            redditModel.name = reddit.Account.Me.Name;
            redditModel.post_id = post.Id;
            redditModel.upvotes = post.UpVotes;
            redditModel.subreddit = post.Subreddit;
            redditModel.CreationDate = DateTime.Now;
            await _redditService.Create(redditModel);
            return View("Confirm");
        }
    }
}
