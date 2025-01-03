using AmazonV02.ApIs.DTOS;
using AmazonV02.ApIs.Errors;
using AmazonV02.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazonV02.ApIs.Controllers
{
	
	public class PaymentsController : ApiBaseController
	{
		private readonly IPaymentService _paymentService;

		public PaymentsController(IPaymentService paymentService)
        {
			_paymentService = paymentService;
		}
		[HttpPost]
		public async Task<ActionResult<CustomerBasketDto>> CreateorUpdatePaymentIntend(string basketId)
		{
			var basket= await _paymentService.CreateOrUpdatePaymentIntendId(basketId);
			if (basket is null) return BadRequest(new ApiResponse(400, "A Problem with your Basket"));
			return Ok(basket);
		}
    }
}
