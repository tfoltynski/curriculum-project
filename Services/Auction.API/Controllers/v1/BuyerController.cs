using Auction.API.Features.Product.Commands.PlaceBid;
using Auction.API.Features.Product.Commands.UpdateBid;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Auction.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IMediator mediator;

        public BuyerController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("place-bid")]
        public async Task<ActionResult> AddProduct([FromBody] PlaceBidCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("update-bid/{productId}/{buyerEmailId}/{newBidAmount}")]
        public async Task<ActionResult> AddProduct([FromRoute] string productId, [FromRoute] string buyerEmailId, [FromRoute] int newBidAmount)
        {
            var command = new UpdateBidCommand { BidAmount = newBidAmount, ProductId = productId, UserEmail = buyerEmailId };
            return Ok(await mediator.Send(command));
        }
    }
}
