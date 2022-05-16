using Auction.API.Features.Product.Events;
using Auction.View.API.Entities;
using Auction.View.API.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Auction.View.API.EventBusConsumer
{
    public sealed class BidUpdatedConsumer : IConsumer<BidUpdated>
    {
        private readonly IProductRepository productRepository;
        private readonly IBidRepository bidRepository;

        public BidUpdatedConsumer(IProductRepository productRepository, IBidRepository bidRepository)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.bidRepository = bidRepository ?? throw new ArgumentNullException(nameof(bidRepository));
        }

        public async Task Consume(ConsumeContext<BidUpdated> context)
        {
            var product = await productRepository.GetByIdAsync(p => p.ProductId == context.Message.ProductId, p => p.Include(p => p.Bids).ThenInclude(p => p.BuyerInformation));
            if (product == null) return;

            var bid = product.Bids.FirstOrDefault(p => p.BuyerInformation.Email == context.Message.UserEmail);
            bid.Amount = context.Message.Amount;
            await bidRepository.UpdateAsync(bid);
        }
    }
}
