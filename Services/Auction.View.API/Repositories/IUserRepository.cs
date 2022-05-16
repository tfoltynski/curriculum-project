using Auction.View.API.Entities;
using System.Threading.Tasks;

namespace Auction.View.API.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
