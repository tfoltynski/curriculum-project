using Auction.SharedKernel.Exceptions;
using Auction.SharedKernel.Infrastructure;
using Auction.SharedKernel.Messaging;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Auction.API.Features.Product.Commands.DeleteProduct
{
    public sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IRepository<ProductAggregate> repository;

        public DeleteProductCommandHandler(IRepository<ProductAggregate> repository)
        {
            this.repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await repository.GetById(command.ProductId);
            if (product == null) throw new ResourceNotFoundException(ResourceNames.Product, command.ProductId);

            product.Delete(command.ProductId);
            await repository.Save(product, product.Version);
            return Unit.Value;
        }
    }
}
