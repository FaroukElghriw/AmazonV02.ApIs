
using AmazonV02.ApIs.Errors;
using AmazonV02.ApIs.Extensions;
using AmazonV02.ApIs.Helper;
using AmazonV02.ApIs.MiddleWares;
using AmazonV02.Core.Repository;
using AmazonV02.Repository;
using AmazonV02.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
			builder.Services.AddApplicationServices();

	

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

			app.UseAuthorization();
			app.UseStaticFiles();


			app.MapControllers();

			app.Run();
		}
	}
}
