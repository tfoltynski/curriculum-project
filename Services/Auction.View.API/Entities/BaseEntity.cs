using System;

namespace Auction.View.API.Entities
{
    public abstract class BaseEntity
    {
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}