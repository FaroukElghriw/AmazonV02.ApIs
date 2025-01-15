using AmazonV02.Core.Entites.Identity;
using AmazonV02.Repository.Data;
using AmazonV02.Repository.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<AmazonDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnnection"));
			});
			builder.Services.AddDbContext<AppIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});
			 builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
			{
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireDigit = true;
			})
		     .AddEntityFrameworkStores<AppIdentityDbContext>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}