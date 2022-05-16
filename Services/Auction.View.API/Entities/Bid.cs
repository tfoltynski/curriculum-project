namespace Auction.View.API.Entities
{
    public class Bid : BaseEntity
    {
        public int BidId { get; set; }
        public int Amount { get; set; }
        public int UserId { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual User BuyerInformation { get; set; }
    }
}
