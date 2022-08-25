using Auction.API.Features.Product.Events;
using Auction.View.API.Entities;
using Auction.View.API.Hubs;
using Auction.View.API.Models;
using Auction.View.API.Repositories;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Auction.View.API.EventBusConsumer
{
    public sealed class ProductCreatedConsumer : IConsumer<ProductCreated>
    {
        private readonly IProductRepository productRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IHubContext<ProductHub> productHub;

        public ProductCreatedConsumer(IProductRepository productRepository, IUserRepository userRepository, IMapper mapper, IHubContext<ProductHub> productHub)
        {
            this.productRepository = productRepository ?? throw new System.ArgumentNullException(nameof(productRepository));
            this.userRepository = userRepository ?? throw new System.ArgumentNullException(nameof(userRepository));
            this.mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            this.productHub = productHub ?? throw new System.ArgumentNullException(nameof(productHub));
        }

        public async Task Consume(ConsumeContext<ProductCreated> context)
        {
            var product = mapper.Map<Product>(context.Message);

            var user = await userRepository.GetByEmailAsync(context.Message.Email);
            if (user == null) {
                user = mapper.Map<User>(context.Message);
                user = await userRepository.AddAsync(user);
            }

            product.SellerInformation = user;
            await productRepository.AddAsync(product);

            var response = mapper.Map<ShowProductDetailsResponse>(product);
            await productHub.Clients.Group(product.SellerInformation.Email).SendAsync(ProductEvents.ProductCreated, response);
            await productHub.Clients.All.SendAsync(ProductEvents.RefreshProductDropdown, response);
        }
    }
}
