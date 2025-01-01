using AmazonV02.Core;
using AmazonV02.Core.Entites;
using AmazonV02.Core.Entites.Order_Aggregate;
using AmazonV02.Core.Repository;
using AmazonV02.Core.Services;
using AmazonV02.Core.Specifications.OrderSpec;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Services
{
	public class OrderService : IOrderService

	{
		//private readonly IGenericRepository<Product> _productrepo;
		private readonly IBasketRepository _basketRepository;
		
		//private readonly IGenericRepository<DeliveryMethod> _deliverymethodrepo;
		//private readonly IGenericRepository<Order> _orderRepo;

		public OrderService( IGenericRepository<Product> productrepo
			, IBasketRepository basketRepository
			//, IGenericRepository<DeliveryMethod> deliverymethodrepo,
			,IUnitofwork unitofwork	)
        {
			
			_basketRepository = basketRepository;

			Unitofwork = unitofwork;
		}

		public IUnitofwork Unitofwork { get; }

		public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address ShippingAddress)
		{
			var basket = await _basketRepository.GetCustomerBasketAsync(basketId);
			var orderItems= new List<OrderItem>();
			if(basket?.Items?.Count > 0)
			{
				foreach (var item in basket.Items)
				{
					var product = await Unitofwork.Repository<Product>().GetByIdAsync(item.Id);
					var productItemOrdered= new ProductItemOrdered(product.Id,product.Name,product.PictureUrl);
					var orderItem= new OrderItem(product.Price,item.Quantity, productItemOrdered);
					orderItems.Add(orderItem);
				}
			}
			var subTotal = orderItems.Sum(O=>O.Price* O.Quantity);
			var deliverymethod= await Unitofwork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);
			var order = new Order(buyerEmail, ShippingAddress, orderItems, deliverymethod, subTotal,null);
			await Unitofwork.Repository<Order>().Add(order);
			await Unitofwork.Complete();
			return order;

		}

		public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
		{
		   var deliveryMethods= await Unitofwork.Repository<DeliveryMethod>().GetAllAsync();
			return deliveryMethods;
		}

		public async Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
		{
			var spec = new OrderSpecification(buyerEmail, orderId);
			var order = await Unitofwork.Repository<Order>().GetByIdWithSpecAsync(spec);
			return order;
		}

		public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
		{
			var spec= new OrderSpecification(buyerEmail);
			var orders = await Unitofwork.Repository<Order>().GetAllWithSpecAsync(spec);
			return orders;
		}


	}
}
