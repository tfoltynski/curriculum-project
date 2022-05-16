using Auction.View.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.View.API.EntityConfiguration
{
    public sealed class BidEntityTypeConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasKey(x => x.BidId);
            builder.HasOne(x => x.Product).WithMany(x => x.Bids).IsRequired();
            builder.HasOne(x => x.BuyerInformation).WithMany();
        }
    }
}
