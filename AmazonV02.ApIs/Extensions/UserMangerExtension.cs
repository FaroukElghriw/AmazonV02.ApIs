using AmazonV02.Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AmazonV02.ApIs.Extensions
{
	public static class UserMangerExtension
	{
		public static async Task<AppUser?> FindUserAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal principal)
		{
			var email= principal.FindFirstValue(ClaimTypes.Email);
			var user= await userManager.Users.Include(u=> u.Address).FirstOrDefaultAsync(x => x.UserName == email);
			return user;


		}
	}
}
