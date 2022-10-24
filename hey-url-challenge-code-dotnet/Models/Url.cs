using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hey_url_challenge_code_dotnet.Models
{
    public class Url
    {
        public Guid Id { get; set; }
        public string ShortUrl { get; set; }
        [Required]
        public string OriginalUrl { get; set; }
        public DateTime DateCreation { get; set; }
        public int Count { get; set; }
        public List<UrlHistory> UrlHistory { get; set; }
        
    }
}
