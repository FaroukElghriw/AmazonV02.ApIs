using AmazonV02.ApIs.DTOS;
using AmazonV02.Core.Entites;
using AutoMapper;

namespace AmazonV02.ApIs.Helper
{
	public class MappingProfilies:Profile
	{
        public MappingProfilies()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(D => D.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(D => D.ProductType, O => O.MapFrom(S => S.ProductType.Name))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPictureUrlResolve>());

            CreateMap<CustomerBasketDto, CustomerBasket>()
                .ForMember(D=>D.Items, O=>O.MapFrom(S=>S.Items));
            CreateMap<BasketItemDto, BasketItem>();

		}
    }
}
