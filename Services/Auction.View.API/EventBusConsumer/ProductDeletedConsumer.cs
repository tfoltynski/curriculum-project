using Auction.API.Features.Product.Events;
using Auction.View.API.Repositories;
using MassTransit;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Auction.View.API.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Auction.View.API.EventBusConsumer
{
    public sealed class ProductDeletedConsumer : IConsumer<ProductDeleted>
    {
        private readonly IProductRepository productRepository;
        private readonly IBidRepository bidRepository;
        private readonly IUserRepository userRepository;

        public ProductDeletedConsumer(IProductRepository productRepository, IBidRepository bidRepository, IUserRepository userRepository)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.bidRepository = bidRepository ?? throw new ArgumentNullException(nameof(bidRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task Consume(ConsumeContext<ProductDeleted> context)
        {
            var product = await productRepository.GetByIdAsync(p => p.ProductId == context.Message.ProductId, p => p.Include(p => p.SellerInformation));
            if (product != null) await productRepository.DeleteAsync(product);

            var userProducts = await productRepository.GetByUserEmailAsync(product.SellerInformation.Email);
            var userBids = await bidRepository.GetByUserEmailAsync(product.SellerInformation.Email);
            if (!userProducts.Any() && !userBids.Any())
            {
                var user = await userRepository.GetByEmailAsync(product.SellerInformation.Email);
                await userRepository.DeleteAsync(user);
            }
        }
    }
}
