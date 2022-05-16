using Auction.View.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auction.View.API.EntityConfiguration
{
    public sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.Email).UseCollation("SQL_Latin1_General_CP1_CI_AS");
        }
    }
}
