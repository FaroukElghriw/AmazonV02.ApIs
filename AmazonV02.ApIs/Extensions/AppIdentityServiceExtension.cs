using AmazonV02.Core.Entites.Identity;
using AmazonV02.Core.Services;
using AmazonV02.Repository.Data.Identity;
using AmazonV02.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AmazonV02.ApIs.Extensions
{
	public static class AppIdentityServiceExtension
	{
		public static IServiceCollection AddIdentityService(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddScoped<ITokenService, TokenService>();
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options=>
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
				    ValidIssuer= configuration["JWT:VaildIssuer"],
					ValidateAudience = true,
					ValidAudience = configuration["JWT:VaildAduience"],
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))

				});
			services.AddIdentity<AppUser, IdentityRole>(options =>
			{
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireDigit = true;
			})
				.AddEntityFrameworkStores<AppIdentityDbContext>();

			return services;
		}
	}
}
