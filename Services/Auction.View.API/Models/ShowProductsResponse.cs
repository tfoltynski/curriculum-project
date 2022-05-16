using Auction.View.API.Models.Common;
using System;
using System.Collections.Generic;
using static Auction.View.API.Entities.Product;

namespace Auction.View.API.Models
{
    public sealed class ShowProductsResponse
    {
        public PagedList<ProductDto> Products { get; set; }

        public sealed class ProductDto {
            public string ProductId { get; set; }
            public string ProductName { get; set; }
            public string ShortDescription { get; set; }
            public string DetailedDescription { get; set; }
            public Category CategoryType { get; set; }
            public int StartingPrice { get; set; }
            public DateTime BidEndDate { get; set; }
            public int UserId { get; set; }
            public UserDto SellerInformation { get; set; }
            public List<BidDto> Bids { get; set; } = new List<BidDto>();
        }

        public sealed class UserDto : BaseResponse
        {
            public int UserId { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Pin { get; set; }
            public string Phone { get; set; }
        }

        public sealed class BidDto : BaseResponse
        {
            public int BidId { get; set; }
            public int Amount { get; set; }
            public int UserId { get; set; }
            public string ProductId { get; set; }
            public UserDto BuyerInformation { get; set; }
        }
    }
}
