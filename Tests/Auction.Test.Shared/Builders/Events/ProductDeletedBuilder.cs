using Auction.API.Features.Product.Events;
using System;

namespace Auction.Test.Shared.Builders.Events
{
    public class ProductDeletedBuilder : BaseEventBuilder<ProductDeleted, ProductDeletedBuilder>
    {
        public ProductDeletedBuilder()
        {
            var currentDate = DateTime.UtcNow;
            Result = new ProductDeleted("Id1")
            {
                Version = 1,
                CreatedDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, currentDate.Minute, 0),
            };
        }

        protected override ProductDeleted Result { get; set; }
    }
}
