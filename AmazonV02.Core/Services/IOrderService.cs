using AmazonV02.Core.Entites.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Services
{
	public interface IOrderService
	{
		 Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address ShippingAddress);

		Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

		Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail);
	}
}
