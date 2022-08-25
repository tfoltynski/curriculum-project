using System;

namespace Auction.SharedKernel.Events
{
    public abstract record Event : IEvent
    {
        public int Version { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string WebSocketConnectionId { get; set; }
    }
}
