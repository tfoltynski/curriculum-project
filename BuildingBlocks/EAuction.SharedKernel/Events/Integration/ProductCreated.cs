﻿using Auction.SharedKernel.Events;
using System;

namespace Auction.API.Features.Product.Events
{
    public sealed record ProductCreated : Event
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public Category CategoryType { get; set; }
        public int StartingPrice { get; set; }
        public DateTime BidEndDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public enum Category
        {
            Painting,
            Sculptor,
            Ornament
        }
    }
}
