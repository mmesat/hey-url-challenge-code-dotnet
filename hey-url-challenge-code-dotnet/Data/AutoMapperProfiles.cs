using AutoMapper;
using hey_url_challenge_code_dotnet.DTOs;
using hey_url_challenge_code_dotnet.Models;

namespace hey_url_challenge_code_dotnet.Data
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Url, UrlDTO>().ReverseMap();
        }
        
    }
}
