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
		[ProducesResponseType(typeof(OrderToReturnDTO),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDto model)
		{
			var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var shippingaddres = _mapper.Map<AddressDto, Address>(model.ShippingAddress);
			var order = await _orderService.CreateOrderAsync(buyerEmail, model.Basketid, model.DeliveryMethodId, shippingaddres);
			if (order is null) return BadRequest(new ApiResponse(400));
			return Ok(order);

		}
		[HttpGet]
		[ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDTO>) , StatusCodes.Status200OK)]
		[ProducesResponseType(typeof (ApiResponse), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrdersForUser()
		{
			var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var orders= await _orderService.GetOrdersForUserAsync(buyerEmail);
			return Ok(orders);

			
		}
		[ProducesResponseType(typeof(OrderToReturnDTO), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("id")]
		public async Task<ActionResult<OrderToReturnDTO>> GetOrderbyidForUse(int id)
		{
			var buyeremail= User.FindFirstValue(ClaimTypes.Email);

			var order = await _orderService.GetOrderByIdForUserAsync(id, buyeremail);
			return Ok(order);	
		  
		}
		[ProducesResponseType(typeof(DeliveryMethod), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("deliverymethod")]
		public async Task<ActionResult<DeliveryMethod>> GetDeliverymethod()
		{
			var deliveryMethods= await _orderService.GetDeliveryMethodsAsync();
			return Ok(deliveryMethods);
		}
    }
}
