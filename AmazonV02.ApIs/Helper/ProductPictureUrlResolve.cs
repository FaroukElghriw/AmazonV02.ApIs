using AmazonV02.ApIs.DTOS;
using AmazonV02.Core.Entites;
using AutoMapper;

namespace AmazonV02.ApIs.Helper
{
	public class ProductPictureUrlResolve : IValueResolver<Product, ProductToReturnDTO, string>

	{
		private readonly IConfiguration _configuration;

		public ProductPictureUrlResolve(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
		{

			if (!string.IsNullOrEmpty(source.PictureUrl))
				return $"{_configuration["BaseUrl"]}{source.PictureUrl}";

			return string.Empty ;
		}
	}
}
