using hey_url_challenge_code_dotnet.Services;
using HeyUrlChallengeCodeDotnet.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Collections;
using System.Security.Policy;
using System.Threading.Tasks;

namespace hey_url_challenge_code_dotnet.Controllers
{
    [Route("/api")]
    public class UrlApiController : ControllerBase
    {
        private readonly ILogger<UrlsController> _logger;
        private readonly IUrlService _service;

        public UrlApiController(ILogger<UrlsController> logger, IUrlService service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet]
        public async Task <ActionResult<string>> GetAllDetail()
        {
            var jsonres = JsonSerializer.Serialize(await _service.GetAll());
            return jsonres;
        }
    }
}
