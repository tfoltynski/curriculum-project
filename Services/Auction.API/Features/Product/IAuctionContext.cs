using Auction.SharedKernel.Events;
using MongoDB.Driver;

namespace Auction.API.Features.Product
{
    public interface IAuctionContext
    {
        IMongoCollection<EventStoreItem> Products { get; }
    }
}