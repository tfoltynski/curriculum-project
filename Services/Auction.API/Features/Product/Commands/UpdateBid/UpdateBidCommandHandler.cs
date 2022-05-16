using Auction.API.Features.Product.Commands.UpdateBid;
using Auction.SharedKernel.Exceptions;
using Auction.SharedKernel.Infrastructure;
using Auction.SharedKernel.Messaging;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Auction.API.Features.Product.Commands.PlaceBid
{
    public sealed class UpdateBidCommandHandler : ICommandHandler<UpdateBidCommand>
    {
        private readonly IRepository<ProductAggregate> repository;

        public UpdateBidCommandHandler(IRepository<ProductAggregate> repository)
        {
            this.repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        public async Task<Unit> Handle(UpdateBidCommand command, CancellationToken cancellationToken)
        {
            var product = await repository.GetById(command.ProductId);
            if (product == null) throw new ResourceNotFoundException(ResourceNames.Product, command.ProductId);

            product.UpdateBid(command);
            await repository.Save(product, product.Version);
            return Unit.Value;
        }
    }
}
