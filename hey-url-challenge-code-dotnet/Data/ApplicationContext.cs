using hey_url_challenge_code_dotnet.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;

namespace HeyUrlChallengeCodeDotnet.Data
{
    public class ApplicationContext : DbContext
    {
        //protected override void OnConfiguring
        //(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseInMemoryDatabase(databaseName: "HeyUrlDB");
        //}

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }


        public DbSet<Url> Urls { get; set; }
        public DbSet<UrlHistory> UrlHistories { get; set; }


    }
}