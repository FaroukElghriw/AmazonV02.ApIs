using AmazonV02.Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Core.Services
{
	public interface ITokenService
	{
		public Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> userManager);
	}
}
