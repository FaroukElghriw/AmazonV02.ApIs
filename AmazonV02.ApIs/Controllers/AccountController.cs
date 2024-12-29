using AmazonV02.ApIs.DTOS;
using AmazonV02.ApIs.Errors;
using AmazonV02.ApIs.Extensions;
using AmazonV02.Core.Entites.Identity;
using AmazonV02.Core.Services;
using AutoMapper;
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
		private readonly IMapper _mapper;

		public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager,ITokenService tokenService,IMapper mapper)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
			_mapper = mapper;
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
			if (CheackEmailAsync(model.Email).Result.Value) return BadRequest(new ApiValidationErrorResponse() { Errors = new string []  { "Email is exist" } });
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
		[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet("Address")]
		public async Task<ActionResult<AddressDto>> GetUserAddress()
		{
			var user = await _userManager.FindUserAddressAsync(User);
			var mappedAddres = _mapper.Map<Address, AddressDto>(user.Address);

			return Ok(mappedAddres);
		}
		[HttpPut]
		public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto model)
		{
			var addres= _mapper.Map<AddressDto, Address>(model);
			var user = await _userManager.FindUserAddressAsync(User);
			addres.Id = user.Address.Id;
			user.Address = addres;
			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded) return BadRequest(new ApiResponse(400));
			return Ok(model);
		}

		[HttpGet("emailexist")]
		public async Task<ActionResult<bool>> CheackEmailAsync(string email)
		{
			return await _userManager.FindByEmailAsync(email) is not null; 
		}

    }
}
