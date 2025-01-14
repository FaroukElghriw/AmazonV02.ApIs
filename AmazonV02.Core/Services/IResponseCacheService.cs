using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Services
{
	public interface IResponseCacheService
	{
		Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeSpan);

		Task<string> GetCacheResponse(string cacheKey);
	}
}
