using Auction.API.Features;
using Auction.API.Features.Product;
using Auction.SharedKernel.Exceptions;
using Auction.SharedKernel.Infrastructure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeleteProduct
{
    public class DeleteProduct
    {
        private readonly IRepository<ProductAggregate> repository;
        private readonly IConfiguration configuration;
        private static HttpClient httpClient = new HttpClient();

        public DeleteProduct(IRepository<ProductAggregate> repository, IConfiguration configuration)
        {
            this.repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [Function("DeleteProduct")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "delete/{productId}")] HttpRequestData req,
            string productId, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger<DeleteProduct>();
            logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var product = await repository.GetById(productId);
                if (product == null) throw new ResourceNotFoundException(ResourceNames.Product, productId);

                product.Delete(productId);
                await repository.Save(product, product.Version);
                await httpClient.PostAsync(configuration.GetValue<string>("LogicAppTriggerUrl"), new StringContent($"{{\"productId\": \"{productId}\"}}", Encoding.UTF8, "application/json"));
            }
            catch (ApiException exception)
            {
                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                await response.WriteAsJsonAsync(new
                {
                    title = exception.Message,
                    status = exception.Status,
                    details = exception.Details
                }, HttpStatusCode.BadRequest);
                return response;
            }
            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
