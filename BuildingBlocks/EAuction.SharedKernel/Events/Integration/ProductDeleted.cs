using Auction.SharedKernel.Events;

namespace Auction.API.Features.Product.Events
{
    public sealed record ProductDeleted(string ProductId) : Event;
}