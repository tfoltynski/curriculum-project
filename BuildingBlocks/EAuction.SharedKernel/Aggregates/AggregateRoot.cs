using Auction.SharedKernel.Events;
using Auction.SharedKernel.Infrastructure;
using System.Collections.Generic;

namespace Auction.SharedKernel.Aggregates
{
    public abstract class AggregateRoot
    {
        private readonly List<Event> _changes = new List<Event>();

        public string Id { get; protected set; }
        public int Version { get; private set; }
        public bool Active { get; protected set; }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            foreach (var e in history) ApplyChange(e, false);
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        // push atomic aggregate changes to local history for further processing (EventStore.SaveEvents)
        private void ApplyChange(Event @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);

            if (isNew) _changes.Add(@event);
            else Version = @event.Version; // only existing events can modify version in aggregate root
        }
    }
}
