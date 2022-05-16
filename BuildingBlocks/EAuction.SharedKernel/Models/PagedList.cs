using System;
using System.Collections.Generic;

namespace Auction.View.API.Models.Common
{
    public class PagedList<T>
    {
        public PagedList(int totalCount, int currentPage, int pageSize)
        {
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public IList<T> Results { get; set; } = new List<T>();
    }
}
