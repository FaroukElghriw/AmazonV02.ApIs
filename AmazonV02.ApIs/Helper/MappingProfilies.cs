using AmazonV02.ApIs.DTOS;
using AmazonV02.Core.Entites;
using AmazonV02.Core.Entites.Identity;
using AmazonV02.Core.Entites.Order_Aggregate;
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

            CreateMap<AmazonV02.Core.Entites.Order_Aggregate.Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, AmazonV02.Core.Entites.Order_Aggregate.Address>();


			CreateMap<Order, OrderToReturnDTO>()
				.ForMember(d => d.DeliveryMethod, O => O.MapFrom(S => S.DeliveryMethod.ShortName))
				.ForMember(d => d.DeliveryMethodCost, O => O.MapFrom(S => S.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId, O => O.MapFrom(S => S.Product.ProductId))
                .ForMember(d => d.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(d => d.PictureUrl, O => O.MapFrom(S => S.Product.ProductPicture));
				//.ForMember(d => d.PictureUrl, O => O.MapFrom<Order>());

		}
    }
}
