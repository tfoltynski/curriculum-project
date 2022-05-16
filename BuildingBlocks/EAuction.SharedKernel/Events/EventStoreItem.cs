using System;

namespace Auction.SharedKernel.Events
{
    public sealed class EventStoreItem
    {
        public DateTime CreatedAt { get; set; }
        public string Data { get; set; }
        public string EventName { get; set; }
        public string AggregateId { get; set; }
        public int Version { get; set; }
    }
}
