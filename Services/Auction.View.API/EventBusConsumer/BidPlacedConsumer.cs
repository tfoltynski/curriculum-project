using Auction.API.Features.Product.Events;
using Auction.View.API.Entities;
using Auction.View.API.Repositories;
using AutoMapper;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Auction.View.API.EventBusConsumer
{
    public sealed class BidPlacedConsumer : IConsumer<BidPlaced>
    {
        private readonly IProductRepository productRepository;
        private readonly IBidRepository bidRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public BidPlacedConsumer(IProductRepository productRepository, IBidRepository bidRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.bidRepository = bidRepository ?? throw new ArgumentNullException(nameof(bidRepository));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            var product = await productRepository.GetByIdAsync(context.Message.ProductId);
            if (product == null) return;

            var user = await userRepository.GetByEmailAsync(context.Message.Email);
            if (user == null)
            {
                user = mapper.Map<User>(context.Message);
                user = await userRepository.AddAsync(user);
            }

            var bid = mapper.Map<Bid>(context.Message);
            bid.UserId = user.UserId;
            await bidRepository.AddAsync(bid);
        }
    }
}
