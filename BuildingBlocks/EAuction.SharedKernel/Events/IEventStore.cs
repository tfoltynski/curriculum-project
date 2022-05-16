using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.SharedKernel.Events
{
    public interface IEventStore
    {
        Task SaveEvents(string aggregateId, IEnumerable<Event> events, int expectedVersion);
        Task<List<Event>> GetEventsForAggregate(string aggregateId);
    }
}
