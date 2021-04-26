using AutoMapper;
using MP.MKKing.API.DTOs;
using MP.MKKing.Core.Models;
using MP.MKKing.Core.Models.Identity;

namespace MP.MKKing.API.Helpers
{
    /// <summary>
    /// A class to map DTOs
    /// </summary>
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(source => source.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(source => source.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }
    }
}