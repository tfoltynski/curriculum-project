using Auction.SharedKernel.Events;

namespace Auction.API.Features.Product.Events
{
    public sealed record BidUpdated(string ProductId, string UserEmail, int Amount) : Event;
}
