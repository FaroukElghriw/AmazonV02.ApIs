using AmazonV02.ApIs.DTOS;
using AmazonV02.ApIs.Errors;
using AmazonV02.ApIs.Helper;
using AmazonV02.Core.Entites;
using AmazonV02.Core.Repository;
using AmazonV02.Core.Specifications.ProductSpec;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonV02.ApIs.Controllers
{

    public class ProductController : ApiBaseController
	{
		private readonly IGenericRepository<Product> _repository;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<ProductBrand> _brands;
		private readonly IGenericRepository<ProductType> _types;

		public ProductController(IGenericRepository<Product> repository,
			IMapper mapper,
			IGenericRepository<ProductBrand> brands,
			IGenericRepository<ProductType> types)
		{
			_repository = repository;
			_mapper = mapper;
			_brands = brands;
			_types = types;
		}

		[ProducesResponseType(typeof(IReadOnlyList<ProductToReturnDTO>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDTO>>> Getall([FromQuery] ProductSpecParms parms)
		{
			var spec = new ProductWithBrandandTypeSpecification(parms);

			var products= await _repository.GetAllWithSpecAsync(spec);
			var prodcutsMapped = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);
			var specCount = new ProductWithFilterForCountSpec(parms);
			var count = await _repository.GetCountWithSpec(specCount);
			return Ok(new Pagination<ProductToReturnDTO>(parms.PageIndex,parms.PageSize,prodcutsMapped,count));
		}
		[ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetbyId(int id)
		{
			var product = await _repository.GetByIdAsync(id);
			return Ok(product);
		}
		[ProducesResponseType(typeof(IReadOnlyList<ProductBrand>),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await _brands.GetAllAsync();

			return Ok(brands);
		}
		[ProducesResponseType(typeof(IReadOnlyList<ProductType>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("types")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetTypes()
		{
			var types = await _types.GetAllAsync();

			return Ok(types);
		}


	}
}
