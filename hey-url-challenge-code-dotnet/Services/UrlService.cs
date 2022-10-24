using AutoMapper;
using hey_url_challenge_code_dotnet.Models;
using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.EntityFrameworkCore;
using Shyjus.BrowserDetection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Services
{
    public class UrlService : IUrlService
    {
        private readonly ApplicationContext context;
        //private readonly IMapper mapper;

        public UrlService(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task SaveUrl(string url, string newUrl)
        {

            Url modelUrl = new Url();
            modelUrl.OriginalUrl = url;
            modelUrl.Id = new Guid();
            modelUrl.ShortUrl = newUrl;
            modelUrl.Count = 0;
            modelUrl.DateCreation = DateTime.Now;
            context.Add(modelUrl);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Url>> GetAll()
        {
            return  await context.Urls.ToListAsync();

        }

        public async Task<string> Update(string id, string OS, string Browser)
        {
            var history = new UrlHistory
            {
                Id = new Guid(),
                IdUrl = Guid.Parse(id),
                BrowserName = Browser,
                OS = OS,
                DateClick = DateTime.Now
            };
            var result = context.Urls.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            result.Result.Count = result.Result.Count + 1;
            //result.Result.UrlHistory.Add(history);
            context.Add(history);
            //result.Result.BrowserName = Browser;
            //result.Result.OS = OS;
            await context.SaveChangesAsync();
            return result.Result.OriginalUrl;
        }

        public async Task<Url> Get(string id)
        {
            return await context.Urls.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
        }

        public Dictionary<string,int> DailyClicks(string id)
        {
            return context.UrlHistories.Where(x => x.DateClick.Month == DateTime.UtcNow.Month && x.IdUrl == Guid.Parse(id))
                .GroupBy(x => x.DateClick.Day)
                .Select(x => new { Day = x.Key, Value = x.Count() })
                .OrderBy(x => x.Day)
                .ToDictionary(x => x.Day.ToString(), y => y.Value);
        }

        public Dictionary<string, int> BrowseClicks(string id)
        {
            return context.UrlHistories.Where(x => x.IdUrl == Guid.Parse(id))
                 .GroupBy(x => x.BrowserName)
                 .Select(x => new { Browser = x.Key, Value = x.Count() })
                 .ToDictionary(x => x.Browser, y => y.Value);
        }

        public Dictionary<string, int> PlataformsClicks(string id)
        {
            return context.UrlHistories.Where(x => x.IdUrl == Guid.Parse(id))
                .GroupBy(x => x.OS)
                .Select(x => new { OS = x.Key, Value = x.Count() })
                .ToDictionary(x => x.OS, y => y.Value);
        }


        public async Task<IEnumerable<Url>> GetAllDetail(string id)
        {
            return await context.Urls.ToListAsync();
        }
    }
}
