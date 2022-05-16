using Auction.API.Features.Product.Events;
using System;

namespace Auction.Test.Shared.Builders.Events
{
    public class BidUpdatedBuilder : BaseEventBuilder<BidUpdated, BidUpdatedBuilder>
    {
        public BidUpdatedBuilder()
        {
            var currentDate = DateTime.UtcNow;
            Result = new BidUpdated("Id1", "first@mail.com", 10)
            {
                Version = 1,
                CreatedDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, currentDate.Minute, 0),
            };
        }

        public BidUpdatedBuilder SetAmount(int amount)
        {
            Result = new BidUpdated(Result.ProductId, Result.UserEmail, amount)
            {
                Version = Result.Version,
                CreatedDate = Result.CreatedDate,
            };
            return this;
        }

        protected override BidUpdated Result { get; set; }
    }
}
