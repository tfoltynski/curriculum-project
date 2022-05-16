using Auction.SharedKernel.Infrastructure;
using Auction.SharedKernel.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Auction.API.Features.Product.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, string>
    {
        private readonly IRepository<ProductAggregate> repository;

        public CreateProductCommandHandler(IRepository<ProductAggregate> repository)
        {
            this.repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        public async Task<string> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var aggregate = new ProductAggregate(command);
            await repository.Save(aggregate, -1);
            return aggregate.Id;
        }
    }
}
