namespace Auction.View.API.Models.Common
{
    public class PaginationInput
    {
        private const int maxPageSize = 100;
        private int pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get => pageSize; set => pageSize = value > maxPageSize ? maxPageSize : value; }
    }
}
