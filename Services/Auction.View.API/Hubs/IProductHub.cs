using Auction.View.API.Entities;
using System.Threading.Tasks;

namespace Auction.View.API.Hubs
{
    public interface IProductHub
    {
        Task ProductCreated(Product product);
    }
}
