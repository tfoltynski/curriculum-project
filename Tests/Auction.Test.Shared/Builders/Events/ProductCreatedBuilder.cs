using Auction.API.Features.Product.Events;
using System;
using static Auction.API.Features.Product.Events.ProductCreated;

namespace Auction.Test.Shared.Builders.Events
{
    public class ProductCreatedBuilder : BaseEventBuilder<ProductCreated, ProductCreatedBuilder>
    {
        public ProductCreatedBuilder()
        {
            var currentDate = DateTime.UtcNow;
            Result = new ProductCreated
            {
                Id = "id1",
                BidEndDate = DateTime.UtcNow.AddDays(2),
                CategoryType = Category.Ornament,
                DetailedDescription = "Det",
                ProductName = "Product",
                ShortDescription = "ShortDesc",
                StartingPrice = 10,
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

        protected override ProductCreated Result { get; set; }
    }
}
