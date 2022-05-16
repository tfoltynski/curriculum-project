using Auction.View.API.Entities;
using Auction.View.API.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.View.API.Repositories.Implementation
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AuctionContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Product>> GetByUserEmailAsync(string email)
        {
            return await dbContext.Set<Product>().AsNoTracking().Where(p => p.SellerInformation.Email == email).ToListAsync();
        }
    }
}