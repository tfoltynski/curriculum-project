using Auction.API.Features.Product.Commands.CreateProduct;
using MassTransit.Mediator;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Auction.API.Hubs
{
    public class ProductHub : Hub<IProductHub>
    {
        private readonly IMediator mediator;

        public ProductHub(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task CreateProduct(CreateProductCommand command)
        {
            command.ConnectionId = Context.ConnectionId;
            await mediator.Send(command);
        }
    }
}
