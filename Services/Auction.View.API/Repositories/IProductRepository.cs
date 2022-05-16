using Auction.View.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.View.API.Repositories
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<IEnumerable<Product>> GetByUserEmailAsync(string email);
    }
}