using Auction.API.Features.Product;
using Auction.SharedKernel.Events;
using Auction.SharedKernel.Infrastructure;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization.Conventions;

namespace DeleteProduct
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureAppConfiguration(c =>
                {
                    c.AddJsonFile($"local.settings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((appBuilder, services) =>
                {
                    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                    services.AddScoped<IAuctionContext, AuctionContext>();
                    services.AddScoped<IEventStore, EventStore>();

                    var configuration = appBuilder.Configuration;
                    services.AddMassTransit(config =>
                    {
                        config.UsingAzureServiceBus((context, cfg) =>
                        {
                            cfg.Host(configuration.GetValue<string>("EventBusSettings:HostAddress"));
                        });
                    });

                    var conventionpack = new ConventionPack() { new IgnoreExtraElementsConvention(true) };
                    ConventionRegistry.Register("IgnoreExtraElements", conventionpack, type => true);
                })
                .Build();

            host.Run();
        }
    }
}