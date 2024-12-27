
using AmazonV02.ApIs.Errors;
using AmazonV02.ApIs.Extensions;
using AmazonV02.ApIs.Helper;
using AmazonV02.ApIs.MiddleWares;
using AmazonV02.Core.Entites.Identity;
using AmazonV02.Core.Repository;
using AmazonV02.Repository;
using AmazonV02.Repository.Data;
using AmazonV02.Repository.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace AmazonV02.ApIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddSwaggerServices();
			builder.Services.AddDbContext<AmazonDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnnection"));
			});
			builder.Services.AddDbContext<AppIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});
			builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
			{
				var connection = builder.Configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection);
			});
			builder.Services.AddApplicationServices();
			builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
			builder.Services.AddIdentityService(builder.Configuration);
		
		

	

			var app = builder.Build();

			#region Updata Database
		using	var Scope = app.Services.CreateScope();
			var service = Scope.ServiceProvider;
			var loggerFacrory= service.GetRequiredService<ILoggerFactory>();
			try
			{
				var dbContext= service.GetRequiredService<AmazonDbContext>();
				await dbContext.Database.MigrateAsync();
				await AmazonDbcontextDataSeed.DataSeedingasync(dbContext);
				var identityDbContext = service.GetRequiredService<AppIdentityDbContext>();
				await identityDbContext.Database.MigrateAsync();
				var dbContextdata= service.GetRequiredService<UserManager<AppUser>>();
				await AppIdentityDbContextDataSeed.SeedDataAsync(dbContextdata);
				
		
			}
			catch (Exception ex) 
			{
				var logger= loggerFacrory.CreateLogger<Program>();
				logger.LogError(ex, "An Error Occoured when make a Mirgrations");

			}
			#endregion
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddleware();
			}
			app.UseMiddleware<ExceptionMiddleWare>();
			app.UseStatusCodePagesWithReExecute("/errors/{0}");
			app.UseHttpsRedirection();
			app.UseAuthentication();

			app.UseAuthorization();
			app.UseStaticFiles();


			app.MapControllers();

			app.Run();
		}
	}
}
