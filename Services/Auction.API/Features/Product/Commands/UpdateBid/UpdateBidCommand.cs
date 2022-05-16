using Auction.SharedKernel.Messaging;

namespace Auction.API.Features.Product.Commands.UpdateBid
{
    public sealed class UpdateBidCommand : ICommand
    {
        public string ProductId { get; set; }
        public string UserEmail { get; set; }
        public int BidAmount { get; set; }
    }
}
