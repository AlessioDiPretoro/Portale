using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Portale.Data;
using Portale.Settings;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using static Portale.Settings.UploadImg;
using Microsoft.AspNetCore.HttpLogging;
using Portale.Models;
using Microsoft.EntityFrameworkCore;
using static Portale.Controllers.SearchController;

namespace Portale.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly JwtSettings _jwtSettings;
		private readonly IAuthorizationHandlerProvider _authorizationHandlerProvider;
		private readonly ILogger<AccountsController> _logger;

		public AccountsController(
			ApplicationDbContext context,
			IAuthorizationHandlerProvider authorizationHandlerProvider,
			IOptions<JwtSettings> JwtSettingsOptions,
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			ILogger<AccountsController> logger
			)
		{
			_context = context;
			_authorizationHandlerProvider = authorizationHandlerProvider;
			_jwtSettings = JwtSettingsOptions.Value;
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
		}

		#region Registration

		public class UserRegistrationInput
		{
			public string Email { get; set; }
			public string Username { get; set; }
			public string Password { get; set; }
			public string? Country { get; set; }
			public string? City { get; set; }
			public string? Address { get; set; }
			public string? Cap { get; set; }
			public string? Description { get; set; }
		}

		public class RegistrationResponseDto
		{
			public bool IsSuccessfulRegistration { get; set; }
			public IEnumerable<string>? Errors { get; set; }
		}

		[HttpPost("registrer")]
		public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationInput userForRegistration)
		{
			if (userForRegistration == null || !ModelState.IsValid)
				return BadRequest();

			var user = new ApplicationUser
			{
				UserName = userForRegistration.Username,
				Email = userForRegistration.Email,
				Description = userForRegistration.Description,
				Country = userForRegistration.Country,
				City = userForRegistration.City,
				Address = userForRegistration.Address,
				Cap = userForRegistration.Cap,
			};
			var result = await _userManager.CreateAsync(user, userForRegistration.Password);
			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description);

				return BadRequest(new RegistrationResponseDto { Errors = errors });
			}

			return StatusCode(201);
		}

		#endregion Registration

		#region Login

		public class UserLoginInput
		{
			public string Email { get; set; }
			public string Username { get; set; }
			public string Password { get; set; }
		}

		public class UserLoginResponse
		{
			public string TokenType { get; set; }
			public string AccessToken { get; set; }
			public string ExpiresIn { get; set; }
			public string RefreshToken { get; set; }
		}

		[HttpPost("loginJwt")]
		public async Task<IActionResult> LoginUser([FromBody] UserLoginInput input)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, isPersistent: false, lockoutOnFailure: false);

				if (result.Succeeded)
				{
					var user = await _signInManager.UserManager.FindByNameAsync(input.Email);
					if (user != null)
					{
						var token = GenerateJwtToken(user);
						return Ok(new { token });
					}
				}
				else
				{
					return Unauthorized("Invalid credentials");
				}
			}
			return BadRequest("Invalid model");
		}

		[HttpPost("loginToken")]
		public async Task<IActionResult> LoginUserToken([FromBody] UserLoginInput input)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, isPersistent: false, lockoutOnFailure: false);

				if (result.Succeeded)
				{
					var user = await _signInManager.UserManager.FindByNameAsync(input.Email);
					if (user != null)
					{
						var tokenType = "Bearer";
						//var authenticationInfo = await _userManager.Generate(user, tokenType, "access_token");
						//var accessToken = await _userManager.(user);
						//var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessToken));
						//var refreshToken = await _userManager.GenerateUserTokenAsync(user, tokenType, "refresh_token");
						//var expiresIn = DateTimeOffset.Parse(await HttpContext.GetTokenAsync("expires_at")).ToString();

						//UserLoginResponse response = new UserLoginResponse
						//{
						//	TokenType = tokenType,
						//	AccessToken = symmetricSecurityKey,
						//	RefreshToken = refreshToken,
						//	ExpiresIn = expiresIn
						//};
						return Ok("boh");
					}
					else
					{
						return BadRequest("User not found");
					}
				}
				else
				{
					return Unauthorized("Invalid credentials");
				}
			}
			return BadRequest("Invalid model");
		}

		private string GenerateJwtToken(ApplicationUser user)
		{
			var claims = new[]
			{
			new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
			new Claim(ClaimTypes.Name, user.UserName)
		};

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
			var signinCredential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecitityToken = new JwtSecurityToken(_jwtSettings.Issuer, _jwtSettings.Audience, claims, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(30), signinCredential);

			var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecitityToken);
			return accessToken;
		}

		#endregion Login

		#region Profile Images

		[Authorize]
		[HttpPost("upload-profile")]
		public async Task<IActionResult> UploadImage([FromForm] ImageUploadModel img)
		{
			string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			if (userId == null)
			{
				return BadRequest("User not found");
			}
			string UploadsFolder = "wwwroot/images/profiles";
			if (img.Image == null || img.Image.Length == 0)
			{
				return BadRequest("No immagine recieved.");
			}
			ISettingsResponse response = await UploadImg.UploadImage(img.Image, UploadsFolder);
			if (response.IsSuccess)
			{
				UserProfileImgs userProfileImgs = new UserProfileImgs
				{
					FileName = response.SuccessMessage,
					UserId = userId
				};
				_context.UserProfileImgs.Add(userProfileImgs);
				await _context.SaveChangesAsync();
			}
			return Ok(response);
		}

		private class UserImagesResponse
		{
			public int Id { get; set; }
			public string FileName { get; set; }
		}

		[Authorize]
		[HttpGet("user-profile-imgs")]
		public async Task<IActionResult> UserImages()
		{
			string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			List<UserImagesResponse> userProfileImgs = await _context.UserProfileImgs.
				Where(u => u.UserId == userId)
				.Select(u => new UserImagesResponse { Id = u.Id, FileName = u.FileName })
				.ToListAsync();

			return Ok(userProfileImgs);
		}

		#endregion Profile Images
	}
}