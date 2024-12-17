using AmazonV02.Core.Entites;
using AmazonV02.Core.Repository;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AmazonV02.Repository
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database= redis.GetDatabase();
        }
        public async Task<bool> DeleteCustomerBasket(string basketId)
		{
			return await _database.KeyDeleteAsync(basketId);
		}

		public async Task<CustomerBasket?> GetCustomerBasketAsync(string basketId)
		{
			var basket = await _database.StringGetAsync(basketId);
			return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket?>(basket);
		}

		public async Task<CustomerBasket?> UpdateCustomerBasketAsync(CustomerBasket basket)
		{
			var createOrUpdateBasket= await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(1));
			if(!createOrUpdateBasket) return null;
			return await GetCustomerBasketAsync(basket.Id);
		}
	}
}
