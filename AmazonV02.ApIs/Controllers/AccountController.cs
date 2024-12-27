using AmazonV02.ApIs.DTOS;
using AmazonV02.ApIs.Errors;
using AmazonV02.Core.Entites.Identity;
using AmazonV02.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AmazonV02.ApIs.Controllers
{

	public class AccountController : ApiBaseController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;

		public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
		}

		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var user= await _userManager.FindByEmailAsync(model.Email);
			if (user is null) return Unauthorized(new ApiResponse(401));
			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
			if (!result.Succeeded) return BadRequest(new ApiResponse(400));
			return Ok(new UserDto()
			{
				Email = model.Email,
				DisplayName = model.Email.Split('@')[0],
				Token = await _tokenService.CreateTokenAsync(user,_userManager)
			});
		}
		[HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var user = new AppUser
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				PhoneNumber = model.Phone,
				UserName = model.Email.Split('@')[0]

			};
			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded) return BadRequest(new ApiResponse(400));
			return Ok(new UserDto()
			{
				DisplayName=model.DisplayName,
				Email=model.Email,
				Token= await _tokenService.CreateTokenAsync(user,_userManager)

			});
		}
		
		[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email= User.FindFirstValue(ClaimTypes.Email);
			var user= await _userManager.FindByEmailAsync(email);
			return Ok(new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _tokenService.CreateTokenAsync(user, _userManager)

			});
		}
    }
}
