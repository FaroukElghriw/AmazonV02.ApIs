using AmazonV02.Core;
using AmazonV02.Core.Entites;
using AmazonV02.Core.Entites.Order_Aggregate;
using AmazonV02.Core.Repository;
using AmazonV02.Core.Services;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Services
{
	public class PaymentSerivce : IPaymentService
	{
		private readonly IConfiguration _configuration;
		private readonly IUnitofwork _unitofwork;
		private readonly IBasketRepository _basketRepository;

		public PaymentSerivce(IConfiguration configuration , IUnitofwork unitofwork , IBasketRepository basketRepository)
        {
			_configuration = configuration;
			_unitofwork = unitofwork;
			_basketRepository = basketRepository;
		}
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntendId(string BasketId)
		{
			StripeConfiguration.ApiKey = _configuration["StripeSetting:SecretKey"];
			var basket= await _basketRepository.GetCustomerBasketAsync(BasketId);
			if (basket is null) return null;
			var shiipingPrice = 0m;
			if (basket.DeliveryMethodId.HasValue)
			{
				var deliveryMethod= await _unitofwork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
				basket.ShippingCost = deliveryMethod.Cost;
				shiipingPrice = deliveryMethod.Cost;
			}
			if(basket?.Items?.Count> 0)
			{
                foreach (var item in basket.Items)
                {
					var product = await _unitofwork.Repository<AmazonV02.Core.Entites.Product>().GetByIdAsync(item.Id);
					if(item.Price != product.Price)
						item.Price= product.Price;
                }
            }
			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			if (string.IsNullOrEmpty(basket.PaymentIntendId))
			{
				var options = new PaymentIntentCreateOptions()
				{
					Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)shiipingPrice * 100,
					Currency = "USD",
					PaymentMethodTypes = new List<string>() { "Card" }

				};
				paymentIntent = await service.CreateAsync(options);

				basket.PaymentIntendId = paymentIntent.Id;
				basket.ClinetSecret = paymentIntent.ClientSecret;
			}
			else
			{
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = (long)basket.Items.Sum(item => item.Price * item.Quantity * 100) + (long)shiipingPrice * 100
				};

				await service.UpdateAsync(basket.PaymentIntendId, options);
			}
			await _basketRepository.UpdateCustomerBasketAsync(basket);
			return basket;
			
		   


		}
	}
}
