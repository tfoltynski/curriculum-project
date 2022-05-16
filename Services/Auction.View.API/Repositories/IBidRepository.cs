using Auction.View.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.View.API.Repositories
{
    public interface IBidRepository : IAsyncRepository<Bid>
    {
        Task<IEnumerable<Bid>> GetByUserEmailAsync(string email);
    }
}
