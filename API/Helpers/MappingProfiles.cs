using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Order,OrderDto>().ReverseMap();
            CreateMap<Product , ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o=> o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o=> o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o=>o.MapFrom<ProductUrlResolver>());
            
            // CreateMap<Address , AddressDto>().ReverseMap();
            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasket , CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItemDto , BasketItem>().ReverseMap();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
        }
    }
}