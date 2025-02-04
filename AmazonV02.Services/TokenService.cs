﻿using AmazonV02.Core.Entites.Identity;
using AmazonV02.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AmazonV02.Services
{
	public class TokenService : ITokenService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
        {
			
			_configuration = configuration;
		}
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
		{
			var authClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.GivenName,user.UserName),
				new Claim(ClaimTypes.Email,user.Email),
			};
			var userRoles = await userManager.GetRolesAsync(user);
			foreach (var roler in userRoles)
				authClaims.Add(new Claim(ClaimTypes.Role, roler));
			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
			var Token = new JwtSecurityToken(

				issuer: _configuration["JWT:VaildIssuer"],
				audience: _configuration["JWT:VaildAduience"],
				expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationDays"])),
				claims:authClaims,
				signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
			);

			return new JwtSecurityTokenHandler().WriteToken(Token);

		}
	}
}
