using AmazonV02.ApIs.DTOS;
using AmazonV02.ApIs.Errors;
using AmazonV02.Core.Entites;
using AmazonV02.Core.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonV02.ApIs.Controllers
{
	
	public class BasketController : ApiBaseController
	{
		private readonly IBasketRepository _basketRepo;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepo,IMapper mapper)
        {
			_basketRepo = basketRepo;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId)
		{
			var basket = await _basketRepo.GetCustomerBasketAsync(BasketId);
			return basket is null ? new CustomerBasket(BasketId) : basket;
		}
		[HttpPost]
		public async Task<ActionResult<CustomerBasketDto>> UpdateCustomerBasket(CustomerBasketDto basket)
		{
			var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
			var createOrUpdateBasket= await _basketRepo.UpdateCustomerBasketAsync(mappedBasket);
			if (createOrUpdateBasket is null) return BadRequest(new ApiResponse(400));
			return Ok(createOrUpdateBasket);
		}
		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket(string BasketId)
		{
			return await _basketRepo.DeleteCustomerBasket(BasketId);

		}

	}
}
