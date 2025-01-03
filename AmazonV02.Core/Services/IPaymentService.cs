using AmazonV02.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Services
{
	public interface IPaymentService
	{
		public Task<CustomerBasket> CreateOrUpdatePaymentIntendId(string BasketId);
	}
}
