using Auction.View.API.Entities;
using Auction.View.API.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.View.API.Repositories.Implementation
{
    public sealed class BidRepository : BaseRepository<Bid>, IBidRepository
    {
        public BidRepository(AuctionContext context) : base(context) { }

        public async Task<IEnumerable<Bid>> GetByUserEmailAsync(string email)
        {
            return await dbContext.Set<Bid>().AsNoTracking().Where(p => p.BuyerInformation.Email == email).ToListAsync();
        }
    }
}
