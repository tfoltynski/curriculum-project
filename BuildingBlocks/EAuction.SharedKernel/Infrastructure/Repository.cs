using Auction.SharedKernel.Aggregates;
using Auction.SharedKernel.Events;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.SharedKernel.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventStore storage;

        public Repository(IEventStore storage)
        {
            this.storage = storage;
        }

        public async Task Save(AggregateRoot aggregate, int expectedVersion)
        {
            await storage.SaveEvents(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
        }

        public async Task<T> GetById(string id)
        {
            var obj = new T();
            var events = await storage.GetEventsForAggregate(id);
            if (!events.Any()) return null;

            obj.LoadsFromHistory(events);
            return obj;
        }
    }
}
