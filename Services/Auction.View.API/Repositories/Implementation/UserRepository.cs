using Auction.View.API.Entities;
using Auction.View.API.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Auction.View.API.Repositories.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AuctionContext dbContext) : base(dbContext) { }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await dbContext.Set<User>().FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}
