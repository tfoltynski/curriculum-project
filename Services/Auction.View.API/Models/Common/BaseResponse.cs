using System;

namespace Auction.View.API.Models.Common
{
    public abstract class BaseResponse
    {
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
