using Auction.SharedKernel.Events;

namespace Auction.API.Features.Product.Events
{
    public sealed record BidPlaced : Event
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ProductId { get; set; }
        public int Amount { get; set; }
    }
}
