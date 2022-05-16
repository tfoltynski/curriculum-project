using Auction.API.Features.Product.Commands.CreateProduct;
using Auction.API.Features.Product.Commands.DeleteProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Auction.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IMediator mediator;

        public SellerController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("add-product")]
        public async Task<ActionResult> AddProduct([FromBody] CreateProductCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost("delete/{productId}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] string productId)
        {
            var command = new DeleteProductCommand() { ProductId = productId };
            await mediator.Send(command);
            return Ok();
        }
    }
}
