using Auction.API.Features.Product.Events;
using Auction.View.API.Entities;
using AutoMapper;

namespace Auction.View.API.Mapping
{
    public class EventToEntityProfile : Profile
    {
        public EventToEntityProfile()
        {
            CreateMap<ProductCreated, Product>(MemberList.Destination)
                .ForMember(m => m.ProductId, o => o.MapFrom(s => s.Id));
            CreateMap<ProductCreated, User>(MemberList.Destination);
            CreateMap<BidPlaced, User>(MemberList.Destination);
            CreateMap<BidPlaced, Bid>(MemberList.Destination);
        }
    }
}
