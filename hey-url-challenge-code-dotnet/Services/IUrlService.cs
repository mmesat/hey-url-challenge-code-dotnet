using hey_url_challenge_code_dotnet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Services
{
    public interface IUrlService
    {
        Task SaveUrl(string url, string newUrl);
        Task<IEnumerable<Url>> GetAll();
        Task<string> Update(string id, string OS, string Browser);
        Task<Url> Get(string id);
        Dictionary<string, int> DailyClicks(string id);
        Dictionary<string, int> BrowseClicks(string id);
        Dictionary<string, int> PlataformsClicks(string id);
    }
}
