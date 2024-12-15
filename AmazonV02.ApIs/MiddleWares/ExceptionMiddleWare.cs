﻿using AmazonV02.ApIs.Errors;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace AmazonV02.ApIs.MiddleWares
{
	public class ExceptionMiddleWare
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleWare> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMiddleWare(RequestDelegate next , ILogger<ExceptionMiddleWare> logger , IHostEnvironment env)
        {
			_next = next;
			_logger = logger;
			_env = env;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex) 
			{
				_logger.LogError(ex,ex.Message);
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				var response=_env.IsDevelopment() ?
					 new ApiExceptionError( (int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString()) :
					 new ApiExceptionError((int)HttpStatusCode.InternalServerError, ex.Message);
				var option = new JsonSerializerOptions{ PropertyNamingPolicy= JsonNamingPolicy.CamelCase};
				var json = JsonSerializer.Serialize(response,option);
				await context.Response.WriteAsync(json);


			}
		}
    }
}
