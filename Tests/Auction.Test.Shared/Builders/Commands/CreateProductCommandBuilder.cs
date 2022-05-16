using Auction.API.Features.Product;
using Auction.API.Features.Product.Commands.CreateProduct;
using System;

namespace Auction.Test.Shared.Builders.Commands
{
    public class CreateProductCommandBuilder : IBuilder<CreateProductCommand>
    {
        private CreateProductCommand createProductCommand;

        public CreateProductCommandBuilder()
        {
            createProductCommand = new CreateProductCommand
            {
                BidEndDate = DateTime.UtcNow.AddDays(2),
                CategoryType = Category.Ornament,
                DetailedDescription = "Det",
                ProductName = "Product",
                ShortDescription = "ShortDesc",
                StartingPrice = 10
            };
        }

        public CreateProductCommand GetResult()
        {
            return createProductCommand;
        }

        public void Reset()
        {
            createProductCommand = new CreateProductCommand();
        }

        public CreateProductCommandBuilder SetBidEndDate(DateTime bidEndDate)
        {
            createProductCommand.BidEndDate = bidEndDate;
            return this;
        }

        public CreateProductCommandBuilder WithSellerInformation()
        {
            createProductCommand.SellerInformation = new CreateProductCommand.SellerInformationDto
            {
                Address = "Address",
                City = "City",
                Email = "first@mail.com",
                FirstName = "FirstName",
                LastName = "LastName",
                Phone = "1111111111",
                Pin = "Pin",
                State = "State"
            };
            return this;
        }
    }
}
