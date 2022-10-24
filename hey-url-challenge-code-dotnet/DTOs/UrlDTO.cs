using System.ComponentModel.DataAnnotations;
using System;

namespace hey_url_challenge_code_dotnet.DTOs
{
    public class UrlDTO
    {
        public Guid Id { get; set; }
        public string ShortUrl { get; set; }
        public int Count { get; set; }
        public string OriginalUrl { get; set; }
    }
}
