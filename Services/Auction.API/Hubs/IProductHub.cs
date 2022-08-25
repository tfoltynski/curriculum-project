using Auction.API.Features.Product.Commands.CreateProduct;
using System.Threading.Tasks;

namespace Auction.API.Hubs
{
    public interface IProductHub
    {
        Task CreateProduct(CreateProductCommand command);
    }
}
