using hey_url_challenge_code_dotnet.Models;
using hey_url_challenge_code_dotnet.Services;
using hey_url_challenge_code_dotnet.ViewModels;
using HeyUrlChallengeCodeDotnet.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shyjus.BrowserDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tests
{
    public class UrlsControllerTest
    {
        private Mock<ILogger<UrlsController>> logMock;
        private Mock<IBrowserDetector> browserMock;
        private Mock<IUrlService> serviceMock;
        [SetUp]
        public void Setup()
        {
            logMock = new Mock<ILogger<UrlsController>>();
            browserMock = new Mock<IBrowserDetector>();
            serviceMock = new Mock<IUrlService>();
        }

        [Test]
        public async Task  TestIndex()
        {
            serviceMock.Setup(m => m.GetAll())
            .ReturnsAsync(new List<Url>
            {
                    new Url{OriginalUrl = "http://www.mega.com", ShortUrl = "ABCDE"}
            });

            var controller = new UrlsController(logMock.Object, browserMock.Object, serviceMock.Object);
            var result = await controller.Index() as ViewResult;
            var model = result.Model as HomeViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Urls.Count());
            Assert.AreEqual("http://www.mega.com", model.Urls.First().OriginalUrl);
            Assert.AreEqual("ABCDE", model.Urls.First().ShortUrl);
        }
        [Test]
        public async Task TestHistory()
        {
            var controller = new UrlsController(logMock.Object, browserMock.Object, serviceMock.Object);
            serviceMock.Setup(x => x.Update(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("http://www.mega.com");
            serviceMock.Setup(x => x.DailyClicks(It.IsAny<string>())).Returns(
                new Dictionary<string,int>
                {
                    { "23",1 }
                });
            serviceMock.Setup(x => x.BrowseClicks(It.IsAny<string>())).Returns(
                new Dictionary<string, int>
                {
                    {"Chrome",2 }
                });
            serviceMock.Setup(x => x.PlataformsClicks(It.IsAny<string>())).Returns(
                new Dictionary<string,int>
                {
                    {"Windows",1 }
                });
            browserMock.SetupGet(x => x.Browser.Name).Returns("Chrome");
            browserMock.SetupGet(x => x.Browser.OS).Returns("Windows");
            var res = await controller.Update("981dfb09-f94b-4229-9dde-f99d62517cd8");
            var result = await controller.Show("981dfb09-f94b-4229-9dde-f99d62517cd8") as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model as ShowViewModel;
            Assert.IsNotNull(model);


            Assert.AreEqual(2, model.BrowseClicks["Chrome"]);
            Assert.AreEqual(1, model.PlatformClicks["Windows"]);
            Assert.AreEqual(1, model.DailyClicks["23"]);
        }
    }
}