using Auction.View.API.Extensions;
using Auction.View.API.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Auction.View.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().MigrateDatabase<AuctionContext>().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
