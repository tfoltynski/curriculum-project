using Auction.View.API.Entities;
using Auction.View.API.Models;
using AutoMapper;

namespace Auction.View.API.Mapping
{
    public class EntityToResponseProfile : Profile
    {
        public EntityToResponseProfile()
        {
            CreateMap<Product, ShowProductDetailsResponse>(MemberList.Destination);
            CreateMap<User, ShowProductDetailsResponse.UserDto>(MemberList.Destination);
            CreateMap<Bid, ShowProductDetailsResponse.BidDto>(MemberList.Destination);
            CreateMap<Product, ShowProductsResponse.ProductDto>(MemberList.Destination);
            CreateMap<User, ShowProductsResponse.UserDto>(MemberList.Destination);
            CreateMap<Bid, ShowProductsResponse.BidDto>(MemberList.Destination);
        }
    }
}
