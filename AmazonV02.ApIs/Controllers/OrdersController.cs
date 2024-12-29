using AmazonV02.ApIs.DTOS;
using AmazonV02.ApIs.Errors;
using AmazonV02.Core.Entites.Order_Aggregate;
using AmazonV02.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AmazonV02.ApIs.Controllers
{
	[Authorize]
	public class OrdersController : ApiBaseController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrdersController(IOrderService orderService,IMapper mapper)
        {
			_orderService = orderService;
			_mapper = mapper;
		}

		[HttpPost]
		[ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<Order>> CreateOrder(OrderDto model)
		{
			var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var shippingaddres = _mapper.Map<AddressDto, Address>(model.ShippingAddress);
			var order = await _orderService.CreateOrderAsync(buyerEmail, model.Basketid, model.DeliveryMethodId, shippingaddres);
			if (order is null) return BadRequest(new ApiResponse(400));
			return Ok(order);

		}
    }
}
