using AmazonV02.ApIs.Errors;
using AmazonV02.ApIs.Helper;
using AmazonV02.Core;
using AmazonV02.Core.Repository;
using AmazonV02.Core.Services;
using AmazonV02.Repository;
using AmazonV02.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace AmazonV02.ApIs.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices( this IServiceCollection services)
		{
			services.AddScoped(typeof(IUnitofwork), typeof(Unitofwork));
			services.AddScoped(typeof(IOrderService),typeof(OrderService));
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		    services.AddAutoMapper(typeof(MappingProfilies));
			services.Configure<ApiBehaviorOptions>(
					options =>
					options.InvalidModelStateResponseFactory = (ActionContext) =>
					{
						var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count > 0)
															.SelectMany(P => P.Value.Errors)
															.Select(E => E.ErrorMessage)
															.ToArray();
						var validationErrorReponse = new ApiValidationErrorResponse()
						{
							Errors = errors
						};
						return new BadRequestObjectResult(validationErrorReponse);
					}

				);



			return services;
		}
	}
}
