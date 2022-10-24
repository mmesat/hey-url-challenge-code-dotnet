using System;

namespace hey_url_challenge_code_dotnet.Models
{
    public class UrlHistory
    {
        public Guid Id { get; set; }
        public Guid IdUrl { get; set; }
        public DateTime DateClick { get; set; }
        public string OS { get; set; }
        public string BrowserName { get; set; }
    }
}
