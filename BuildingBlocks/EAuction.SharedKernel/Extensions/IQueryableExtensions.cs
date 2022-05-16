using Auction.View.API.Models.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.SharedKernel.Extensions
{
    public static class IQueryableExtensions
    {
        public async static Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> query, int pageSize, int pageNumber)
        {
            var totalCount = await query.CountAsync();
            var pagedList = new PagedList<T>(totalCount, pageNumber, pageSize);
            var list = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            pagedList.Results = await list.ToListAsync();
            return pagedList;
        }
    }
}
