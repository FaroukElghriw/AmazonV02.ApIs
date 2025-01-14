using AmazonV02.Core.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AmazonV02.Services
{
	public class ResponseCacheService : IResponseCacheService
	{
		private readonly IDatabase _database;

		public ResponseCacheService(IConnectionMultiplexer redis)
        {
			_database= redis.GetDatabase();
		}
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeSpan)
		{
			if (response == null) return;
			var options = new JsonSerializerOptions() { PropertyNamingPolicy= JsonNamingPolicy.CamelCase };
			var serializedResponse= JsonSerializer.Serialize(response,options);
			await _database.StringSetAsync(cacheKey,serializedResponse, timeSpan);
		}

		public async Task<string> GetCacheResponse(string cacheKey)
		{
			var cacheResponse = await _database.StringGetAsync(cacheKey);
			if (cacheResponse.IsNullOrEmpty) return null;
			return cacheResponse;
		}
	}
}
