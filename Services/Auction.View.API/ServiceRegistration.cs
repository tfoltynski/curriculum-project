using Auction.SharedKernel.Events;
using Auction.View.API.EventBusConsumer;
using Auction.View.API.Persistence;
using Auction.View.API.Repositories;
using Auction.View.API.Repositories.Implementation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Auction.View.API
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddAuctionServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDbContext<AuctionContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBidRepository, BidRepository>();

            services.AddScoped<ProductCreatedConsumer>();
            services.AddScoped<ProductDeletedConsumer>();
            services.AddScoped<BidPlacedConsumer>();
            services.AddScoped<BidUpdatedConsumer>();

            services.AddMassTransit(config =>
            {
                config.AddConsumersFromNamespaceContaining<ProductCreatedConsumer>();
                //config.UsingAzureServiceBus((context, cfg) =>
                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetValue<string>("EventBusSettings:HostAddress"));
                    cfg.ReceiveEndpoint(EventBusEndpoints.ProductCreatedEndpoint, c => c.ConfigureConsumer<ProductCreatedConsumer>(context));
                    cfg.ReceiveEndpoint(EventBusEndpoints.ProductDeletedEndpoint, c => c.ConfigureConsumer<ProductDeletedConsumer>(context));
                    cfg.ReceiveEndpoint(EventBusEndpoints.BidPlacedEndpoint, c => c.ConfigureConsumer<BidPlacedConsumer>(context));
                    cfg.ReceiveEndpoint(EventBusEndpoints.BidUpdatedEndpoint, c => c.ConfigureConsumer<BidUpdatedConsumer>(context));
                });
            });

            return services;
        }
    }
}