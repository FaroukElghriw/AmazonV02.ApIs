using AmazonV02.Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Repository.Data.Identity
{
	public static class AppIdentityDbContextDataSeed
	{
		public static async Task SeedDataAsync(UserManager<AppUser> userManager)
		{
			if (!userManager.Users.Any())
			{
				var user = new AppUser()
				{
					DisplayName = "Farouk Khaled",
					Email = "elghriwfarouk@gmail.com",
					PhoneNumber = "01551279956",
					UserName = "Farouk.Khaled"
				};
				await userManager.CreateAsync(user, "Pa$$w0rd");

			}
		}
	}
}
