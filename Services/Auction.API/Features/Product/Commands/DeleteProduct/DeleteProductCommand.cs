using Auction.SharedKernel.Messaging;

namespace Auction.API.Features.Product.Commands.DeleteProduct
{
    public sealed class DeleteProductCommand : ICommand
    {
        public string ProductId { get; set; }
    }
}
