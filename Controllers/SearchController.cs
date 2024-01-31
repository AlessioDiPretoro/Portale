using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portale.Data;
using Portale.Models;

namespace Portale.Controllers
{
	[Authorize]
	//[Route("api/[controller]")]
	[ApiController]
	public class SearchController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public SearchController(ApplicationDbContext context)
		{
			_context = context;
		}

		public class UserResponse
		{
			public string? UserName { get; set; }
			public string? Email { get; set; }
			public string? Description { get; set; }
			public ICollection<UserProfileImgs> ProfileImgs { get; set; } = new List<UserProfileImgs>();
		}

		[HttpGet]
		[Route("api/search-user")]
		public async Task<ActionResult<List<UserResponse>>> GetUsers(string key)
		{
			if (key.Length < 2)
			{
				return NotFound();
			}

			List<UserResponse> additionalInfo = await _context.Users
				.OfType<ApplicationUser>()
				.Where(u => EF.Functions.Like(u.UserName, $"%{key}%") ||
							EF.Functions.Like(u.Email, $"%{key}%") ||
							EF.Functions.Like(u.Description, $"%{key}%"))
				.Select(u => new UserResponse { Description = u.Description, UserName = u.UserName, Email = u.Email, ProfileImgs = u.UserProfileImgs })
				.ToListAsync();

			return additionalInfo;
		}
	}
}