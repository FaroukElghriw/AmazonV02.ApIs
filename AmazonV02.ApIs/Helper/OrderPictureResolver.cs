using AmazonV02.ApIs.DTOS;
using AmazonV02.Core.Entites.Order_Aggregate;
using AutoMapper;
using AutoMapper.Execution;

namespace AmazonV02.ApIs.Helper
{
	public class OrderPictureResolver : IValueResolver<OrderItem, OrderItemDTO, string>
	{
		private readonly IConfiguration _configuration;

		public OrderPictureResolver(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.Product.ProductPicture))
				return $"{_configuration["BaseUrl"]}{source.Product.ProductPicture}";

			return string.Empty;
		}
	}
}
