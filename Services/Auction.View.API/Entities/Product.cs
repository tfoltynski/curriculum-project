using System;
using System.Collections.Generic;

namespace Auction.View.API.Entities
{
    public class Product : BaseEntity
    {
        public string ProductId { get; set; }
        public string ProductName { get; private set; }
        public string ShortDescription { get; private set; }
        public string DetailedDescription { get; private set; }
        public Category CategoryType { get; private set; }
        public int StartingPrice { get; private set; }
        public DateTime BidEndDate { get; private set; }
        public int UserId { get; set; }
        public virtual User SellerInformation { get; set; }
        public virtual ICollection<Bid> Bids { get; set; } = new HashSet<Bid>();

        public enum Category
        {
            Painting,
            Sculptor,
            Ornament
        }
    }
}
