using AmazonV02.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace AmazonV02.ApIs.Helper
{
	public class CachedAtttibute : Attribute, IAsyncActionFilter
	{
		private readonly int _timeToLive;

		public CachedAtttibute(int timeToLive)
        {
			_timeToLive = timeToLive;
		}
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
			var key = GenerateCacheKeyFromRequest(context.HttpContext.Request);
			var cacheRespone = await cacheService.GetCacheResponse(key);
			if (!string.IsNullOrEmpty(cacheRespone))
			{
				var contentResult = new ContentResult()
				{
					Content = cacheRespone,
					ContentType = "application/json",
					StatusCode=200
				};
				context.Result = contentResult;
				return;
			}
			var executedEbdpiontContext = await next();
			if(executedEbdpiontContext.Result is OkObjectResult objectResult)
			{
				await cacheService.CacheResponseAsync(key, objectResult , TimeSpan.FromSeconds(_timeToLive));
			}
		}

		private string GenerateCacheKeyFromRequest(HttpRequest request)
		{
			var keyBuilder = new StringBuilder();
			keyBuilder.Append(request.Path);
			foreach (var (key, value) in request.Query)
				keyBuilder.Append($"|{key} - {value}|");

			return keyBuilder.ToString();
		}
	}
}
