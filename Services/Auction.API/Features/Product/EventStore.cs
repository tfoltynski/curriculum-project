using Auction.SharedKernel.Events;
using MassTransit;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event = Auction.SharedKernel.Events.Event;

namespace Auction.API.Features.Product
{
    public class EventStore : IEventStore
    {
        private readonly IPublishEndpoint publisher;
        private readonly IAuctionContext context;

        public EventStore(IPublishEndpoint publisher, IAuctionContext auctionContext)
        {
            this.publisher = publisher;
            context = auctionContext;
        }

        public async Task SaveEvents(string aggregateId, IEnumerable<Event> events, int expectedVersion)
        {
            var i = expectedVersion;
            
            foreach (var @event in events)
            {
                i++;
                @event.Version = i;

                await context.Products.InsertOneAsync(new EventStoreItem
                {
                    AggregateId = aggregateId,
                    CreatedAt = DateTime.UtcNow,
                    Data = JsonConvert.SerializeObject(@event, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }),
                    EventName = @event.GetType().Name,
                    Version = i,
                });

                // publish current event to the bus for further processing by subscribers
                await publisher.Publish(@event, @event.GetType());
            }
        }

        // collect all processed events for given aggregate and return them as a list
        // used to build up an aggregate from its history (Domain.LoadsFromHistory)
        public async Task<List<Event>> GetEventsForAggregate(string aggregateId)
        {
            var productEvents = await context.Products.Find(p => p.AggregateId == aggregateId).ToListAsync();
            return productEvents.Select(TransformEvent).ToList();
        }

        private Event TransformEvent(EventStoreItem eventSelected)
        {
            var obj = JsonConvert.DeserializeObject(eventSelected.Data, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            return obj as Event;
        }
    }
}
