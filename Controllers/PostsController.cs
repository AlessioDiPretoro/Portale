using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portale.Data;
using Portale.Models;
using Portale.Settings;
using static Portale.Settings.UploadImg;

namespace Portale.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class PostsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly string PostsFolder = "wwwroot/images/posts";

		public partial class CreatePostRequest
		{
			public string? Name { get; set; }
			public string? Description { get; set; }
			public IEnumerable<IFormFile>? Images { get; set; }
		}

		public PostsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: api/Posts
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Posts>>> GetPosts()
		{
			return await _context.Posts.ToListAsync();
		}

		// GET: api/Posts/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Posts>> GetPosts(int id)
		{
			var posts = await _context.Posts.FindAsync(id);

			if (posts == null)
			{
				return NotFound();
			}

			return posts;
		}

		// PUT: api/Posts/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPosts(int id, Posts posts)
		{
			if (id != posts.Id)
			{
				return BadRequest();
			}

			_context.Entry(posts).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PostsExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Posts create Post
		[HttpPost]
		public async Task<ActionResult<CreatePostRequest>> PostPosts(CreatePostRequest crp)
		{
			Posts posts = new Posts
			{
				UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
				Name = crp.Name,
				Description = crp.Description
			};

			_context.Posts.Add(posts);
			await _context.SaveChangesAsync();
			if (crp.Images != null)
			{
				foreach (var img in crp.Images)
				{
					ISettingsResponse response = await UploadImage(img, PostsFolder);
					if (response.IsSuccess)
					{
						PostImgs postImg = new PostImgs
						{
							Name = response.SuccessMessage,
							PostsId = posts.Id
						};
						_context.PostImgs.Add(postImg);
					}
				}
			}

			await _context.SaveChangesAsync();

			return Ok("Ok post caricato correttamente");
		}

		// DELETE: api/Posts/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePosts(int id)
		{
			var posts = await _context.Posts.FindAsync(id);
			if (posts == null)
			{
				return NotFound();
			}

			_context.Posts.Remove(posts);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool PostsExists(int id)
		{
			return _context.Posts.Any(e => e.Id == id);
		}
	}
}