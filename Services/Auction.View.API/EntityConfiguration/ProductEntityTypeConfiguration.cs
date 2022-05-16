using Auction.View.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.View.API.EntityConfiguration
{
    public sealed class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ProductId);
            builder.HasMany(x => x.Bids).WithOne(x => x.Product);
            builder.HasOne(x => x.SellerInformation).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
