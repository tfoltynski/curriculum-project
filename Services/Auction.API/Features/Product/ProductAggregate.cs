using Auction.API.Features.Product.Commands.CreateProduct;
using Auction.API.Features.Product.Commands.PlaceBid;
using Auction.API.Features.Product.Commands.UpdateBid;
using Auction.API.Features.Product.Events;
using Auction.API.Features.User;
using Auction.SharedKernel.Aggregates;
using Auction.SharedKernel.Exceptions;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auction.API.Features.Product
{
    public sealed class ProductAggregate : AggregateRoot
    {
        public string ProductName { get; private set; }
        public string ShortDescription { get; private set; }
        public string DetailedDescription { get; private set; }
        public Category Category { get; private set; }
        public int StartingPrice { get; private set; }
        public DateTime BidEndDate { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public List<Bid> Bids { get; private set; } = new List<Bid>();
        public UserInformation SellerInformation { get; private set; }

        public ProductAggregate() { }

        public ProductAggregate(CreateProductCommand command)
        {
            if (command.BidEndDate < DateTime.UtcNow) throw new BusinessViolationException("Bid end date should be in the future.");

            var evnt = new ProductCreated
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Address = command.SellerInformation.Address,
                BidEndDate = command.BidEndDate,
                CategoryType = (ProductCreated.Category)command.CategoryType,
                City = command.SellerInformation.City,
                DetailedDescription = command.DetailedDescription,
                Email = command.SellerInformation.Email,
                FirstName = command.SellerInformation.FirstName,
                LastName = command.SellerInformation.LastName,
                Phone = command.SellerInformation.Phone,
                Pin = command.SellerInformation.Pin,
                ProductName = command.ProductName,
                ShortDescription = command.ShortDescription,
                StartingPrice = command.StartingPrice,
                State = command.SellerInformation.State,
            };
            ApplyChange(evnt);
        }

        public void Delete(string productId)
        {
            if (BidEndDate < DateTime.UtcNow) throw new BusinessViolationException("Cannot delete product which auction was completed.");
            if (Bids.Any()) throw new BusinessViolationException("Cannot delete product which has already a bid.");
            if (Active == false) throw new BusinessViolationException("Product was already deleted.");

            ApplyChange(new ProductDeleted(productId));
        }

        public void PlaceBid(PlaceBidCommand command)
        {
            if (Active == false) throw new BusinessViolationException("Cannot bid on product which was deleted.");
            if (BidEndDate < DateTime.UtcNow) throw new BusinessViolationException("Cannot place a bid on product which auction was completed.");

            var userBid = Bids.FirstOrDefault(p => p.BuyerInformation.Email == command.Email);
            if (userBid != null) throw new BusinessViolationException("User already has placed a bid for that product.");

            var evnt = new BidPlaced
            {
                Address = command.Address,
                Amount = command.BidAmount,
                City = command.City,
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Phone = command.Phone,
                Pin = command.Pin,
                ProductId = command.ProductId,
                State = command.State,
            };
            ApplyChange(evnt);
        }

        public void UpdateBid(UpdateBidCommand command)
        {
            if (Active == false) throw new BusinessViolationException("Cannot update bid on product which was deleted.");
            if (BidEndDate < DateTime.UtcNow) throw new BusinessViolationException("Cannot update a bid on product which auction was completed.");

            var userBid = Bids.FirstOrDefault(p => p.BuyerInformation.Email == command.UserEmail);
            if (userBid == null) throw new ResourceNotFoundException(ResourceNames.Bid, command.UserEmail);

            ApplyChange(new BidUpdated(command.ProductId, command.UserEmail, command.BidAmount));
        }

        private void Apply(ProductCreated evnt)
        {
            Id = evnt.Id;
            StartingPrice = evnt.StartingPrice;
            BidEndDate = evnt.BidEndDate;
            CreatedDate = evnt.CreatedDate;
            UpdatedDate = evnt.CreatedDate;
            ShortDescription = evnt.ShortDescription;
            DetailedDescription = evnt.DetailedDescription;
            Category = (Category)evnt.CategoryType;
            ProductName = evnt.ProductName;
            SellerInformation = new UserInformation
            {
                Address = evnt.Address,
                City = evnt.City,
                Email = evnt.Email,
                FirstName = evnt.FirstName,
                LastName = evnt.LastName,
                Phone = evnt.Phone,
                Pin = evnt.Pin,
                State = evnt.State,
            };
            Active = true;
        }

        private void Apply(ProductDeleted evnt)
        {
            UpdatedDate = evnt.CreatedDate;
            Active = false;
        }

        private void Apply(BidPlaced evnt)
        {
            Bids.Add(new Bid
            {
                Amount = evnt.Amount,
                BuyerInformation = new UserInformation
                {
                    Address = evnt.Address,
                    City = evnt.City,
                    Email = evnt.Email,
                    FirstName = evnt.FirstName,
                    LastName = evnt.LastName,
                    Phone = evnt.Phone,
                    Pin = evnt.Pin,
                    State = evnt.State,
                },
                CreatedDate = evnt.CreatedDate,
                UpdatedDate = evnt.CreatedDate,
            });
        }

        private void Apply(BidUpdated evnt)
        {
            var userBid = Bids.FirstOrDefault(p => p.BuyerInformation.Email == evnt.UserEmail);
            if (userBid != null)
            {
                userBid.Amount = evnt.Amount;
                userBid.UpdatedDate = evnt.CreatedDate;
            }
        }

        public sealed class Bid
        {
            public int Amount { get; set; }
            public UserInformation BuyerInformation { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime UpdatedDate { get; set; }
        }
    }
}
