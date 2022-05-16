using Auction.API.Features.Product;
using Auction.SharedKernel.Events;
using Auction.SharedKernel.Exceptions;
using Auction.Test.Shared.Builders.Commands;
using Auction.Test.Shared.Builders.Events;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Auction.API.Tests
{
    public class ProductAggregateTests
    {
        [Fact]
        public void CreateProduct_Works()
        {
            var createProductCommand = new CreateProductCommandBuilder().WithSellerInformation().GetResult();

            var testee = new ProductAggregate(createProductCommand);
            Assert.Equal(createProductCommand.ProductName, testee.ProductName);
            Assert.Equal(createProductCommand.DetailedDescription, testee.DetailedDescription);
            Assert.Equal(createProductCommand.StartingPrice, testee.StartingPrice);
            Assert.Equal(createProductCommand.BidEndDate, testee.BidEndDate);
            Assert.Equal(createProductCommand.CategoryType, testee.Category);
            Assert.Equal(createProductCommand.ShortDescription, testee.ShortDescription);
            Assert.Equal(createProductCommand.SellerInformation.Address, testee.SellerInformation.Address);
            Assert.Equal(createProductCommand.SellerInformation.FirstName, testee.SellerInformation.FirstName);
            Assert.Equal(createProductCommand.SellerInformation.LastName, testee.SellerInformation.LastName);
            Assert.Equal(createProductCommand.SellerInformation.Pin, testee.SellerInformation.Pin);
            Assert.Equal(createProductCommand.SellerInformation.State, testee.SellerInformation.State);
            Assert.Equal(createProductCommand.SellerInformation.City, testee.SellerInformation.City);
            Assert.Equal(createProductCommand.SellerInformation.Email, testee.SellerInformation.Email);
            Assert.Equal(createProductCommand.SellerInformation.Phone, testee.SellerInformation.Phone);
        }

        [Fact]
        public void CreateProduct_Throws()
        {
            var createProductCommand = new CreateProductCommandBuilder().SetBidEndDate(new DateTime(2022, 01, 01)).WithSellerInformation().GetResult();

            var ex = Assert.Throws<BusinessViolationException>(() => new ProductAggregate(createProductCommand));
            Assert.Equal(StatusCodes.Status400BadRequest, ex.Status);
            Assert.Equal("Bid end date should be in the future.", ex.Message);
        }

        [Fact]
        public void LoadFromHistory_Works()
        {
            var productCreated = new ProductCreatedBuilder().SetVersion(0).GetResult();
            var bidPlaced = new BidPlacedBuilder().SetVersion(1).GetResult();
            var bidUpdated = new BidUpdatedBuilder().SetVersion(2).SetAmount(50).GetResult();
            var productDeleted = new ProductDeletedBuilder().SetVersion(3).GetResult();
            var eventList = new List<Event> { productCreated, bidPlaced, bidUpdated, productDeleted };

            var product = new ProductAggregate();
            product.LoadsFromHistory(eventList);
            Assert.Equal(productCreated.ProductName, product.ProductName);
            Assert.Equal(productCreated.DetailedDescription, product.DetailedDescription);
            Assert.Equal(productCreated.StartingPrice, product.StartingPrice);
            Assert.Equal(productCreated.BidEndDate, product.BidEndDate);
            Assert.Equal((Category)productCreated.CategoryType, product.Category);
            Assert.Equal(productCreated.ShortDescription, product.ShortDescription);
            Assert.Equal(productCreated.Address, product.SellerInformation.Address);
            Assert.Equal(productCreated.FirstName, product.SellerInformation.FirstName);
            Assert.Equal(productCreated.LastName, product.SellerInformation.LastName);
            Assert.Equal(productCreated.Pin, product.SellerInformation.Pin);
            Assert.Equal(productCreated.State, product.SellerInformation.State);
            Assert.Equal(productCreated.City, product.SellerInformation.City);
            Assert.Equal(productCreated.Email, product.SellerInformation.Email);
            Assert.Equal(productCreated.Phone, product.SellerInformation.Phone);
            Assert.Equal(bidUpdated.Amount, product.Bids.FirstOrDefault().Amount);
            Assert.Equal(productDeleted.Version, product.Version);
            Assert.False(product.Active);
        }
    }
}
