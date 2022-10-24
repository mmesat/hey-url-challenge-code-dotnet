using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Services;
using hey_url_challenge_code_dotnet.ViewModels;
using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shyjus.BrowserDetection;

namespace HeyUrlChallengeCodeDotnet.Controllers
{
    [Route("/")]
    public class UrlsController : Controller
    {
        private readonly ILogger<UrlsController> _logger;
        private static readonly Random getrandom = new Random();
        private readonly IBrowserDetector browserDetector;
        private static Random random = new Random();
        private readonly IUrlService _service;

        public UrlsController(ILogger<UrlsController> logger, IBrowserDetector browserDetector, IUrlService service)
        {
            this.browserDetector = browserDetector;
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel();
            var result = await _service.GetAll();
            return View(new HomeViewModel { Urls = result});
        }
        [HttpPost]
        public async Task<ActionResult> Create(HomeViewModel model)
        {
            Uri uriResult;
            bool result = false;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWX";
            string shortUrl = string.Empty;
            
            if (model != null)
            {
                 result = Uri.TryCreate(model.NewUrl.OriginalUrl, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }

            if (result)
            {
                shortUrl = new string(Enumerable.Repeat(chars, 5).Select(x => x[random.Next(x.Length)]).ToArray());
                await _service.SaveUrl(model.NewUrl.OriginalUrl, shortUrl);
            }
            else
            {
                TempData["Notice"] = "Invalid URL";
                return RedirectToAction("Index");
            }
            await Task.Delay(2500);
            return RedirectToAction("Index");
        }


        [Route("/update/{id}")]
        public async Task<ActionResult> Update(string id)
        {
            var result = await _service.Update(id,this.browserDetector.Browser.OS,this.browserDetector.Browser.Name);
            return Redirect(result);
        }

        [Route("show/{id}")]
        public async Task<IActionResult> Show(string id)
        {
            var result = await _service.Get(id);
            var dailyClicks = _service.DailyClicks(id);
            var browseClicks = _service.BrowseClicks(id);
            var platfromClicks = _service.PlataformsClicks(id);

            return View(new ShowViewModel
            {
                Url = result,
                DailyClicks = dailyClicks,
                BrowseClicks =  browseClicks,
                PlatformClicks = platfromClicks
            });
        }
    }
}