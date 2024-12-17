using AmazonV02.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Repository
{
	public interface IBasketRepository
	{
		Task<CustomerBasket?> GetCustomerBasketAsync(string basketId);
		Task<CustomerBasket?> UpdateCustomerBasketAsync(CustomerBasket basket);

		Task<bool> DeleteCustomerBasket(string basketId);
	}
}
