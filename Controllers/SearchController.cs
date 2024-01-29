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
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<string[]>> GetUsers(string toSearch)
        {
            if (toSearch.Length < 2)
            {
                return NotFound();
            }
            List<string> response = new List<string>();

            List<UserAdditionalInfo> additionalInfo = await _context.Users
                .OfType<UserAdditionalInfo>()
                .Where(u => EF.Functions.Like(u.UserName, $"%{toSearch}%") ||
                            EF.Functions.Like(u.Email, $"%{toSearch}%") ||
                            EF.Functions.Like(u.Description, $"%{toSearch}%"))
                .ToListAsync();

            if (additionalInfo.Count == 0)
            {
                return NotFound();
            }

            foreach (IdentityUser user in additionalInfo)
            {
                response.Add(user.UserName);
            }

            return response.ToArray();
        }
    }
}