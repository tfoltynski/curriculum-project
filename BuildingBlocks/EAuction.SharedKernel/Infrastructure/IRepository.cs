using Auction.SharedKernel.Aggregates;
using System.Threading.Tasks;

namespace Auction.SharedKernel.Infrastructure
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        Task Save(AggregateRoot aggregate, int expectedVersion);
        Task<T> GetById(string id);
    }
}
