using Auction.API.Features.Product.Events;
using System;

namespace Auction.Test.Shared.Builders.Events
{
    public class BidPlacedBuilder : BaseEventBuilder<BidPlaced, BidPlacedBuilder>
    {
        public BidPlacedBuilder()
        {
            var currentDate = DateTime.UtcNow;
            Result = new BidPlaced
            {
                Amount = 10,
                ProductId = "Id1",
                Address = "Address",
                City = "City",
                Email = "first@mail.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Phone = "1111111111",
                Pin = "Pin",
                State = "State",
                Version = 1,
                CreatedDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, currentDate.Minute, 0),
            };
        }

        protected override BidPlaced Result { get; set; }
    }
}
