using Auction.API.Features.Product;
using Auction.SharedKernel.Behaviours;
using Auction.SharedKernel.Events;
using Auction.SharedKernel.Extensions;
using Auction.SharedKernel.Infrastructure;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization.Conventions;

namespace Auction.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((context, cfg) => { cfg.Host(Configuration.GetValue<string>("EventBusSettings:HostAddress")); });
            });

            services.AddScoped<IAuctionContext, AuctionContext>();
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = typeof(Startup).Assembly.GetName().Name, Version = "v1" });
                //c.SwaggerDoc("v2", new OpenApiInfo { Title = typeof(Startup).Assembly.GetName().Name, Version = "v2" });
            });

            var conventionpack = new ConventionPack() { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreExtraElements", conventionpack, type => true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"{typeof(Startup).Assembly.GetName().Name} {description.GroupName.ToUpperInvariant()}");
                    }
                });
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseGlobalExceptionHandling();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
