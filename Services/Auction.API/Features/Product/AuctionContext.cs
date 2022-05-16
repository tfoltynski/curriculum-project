using Auction.SharedKernel.Events;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Auction.API.Features.Product
{
    public sealed class AuctionContext : IAuctionContext
    {
        public AuctionContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<EventStoreItem>(configuration.GetValue<string>("DatabaseSettings:ProductsCollectionName"));

            var indexKeysDefinition = Builders<EventStoreItem>.IndexKeys.Ascending(f => f.AggregateId).Ascending(f => f.Version);
            var createIndexModel = new CreateIndexModel<EventStoreItem>(indexKeysDefinition, new CreateIndexOptions { Unique = true });
            Products.Indexes.CreateOne(createIndexModel); // Unique index on AggregateId and Version prevents concurrent insert scenarios
        }

        public IMongoCollection<EventStoreItem> Products { get; }
    }
}