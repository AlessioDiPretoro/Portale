using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portale.Data;
using Portale.Models;
using Portale.Settings;
using static Portale.Settings.UploadImg;

namespace Portale.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TagsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly string TagsFolder = "wwwroot/images/tags";

		public TagsController(ApplicationDbContext context)
		{
			_context = context;
		}

		public class CreateTagRequest
		{
			public string Name { get; set; } = null!;
			public string? Description { get; set; }
			public IFormFile? Image { get; set; }
		}

		// GET: api/Tags
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Tags>>> GetTags()
		{
			return await _context.Tags.Include(p => p.PostTags).ToListAsync();
		}

		// GET: api/Tags/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Tags>> GetTags(int id)
		{
			var tags = await _context.Tags.FindAsync(id);

			if (tags == null)
			{
				return NotFound();
			}

			return tags;
		}

		// PUT: api/Tags/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTags(int id, Tags tags)
		{
			if (id != tags.Id)
			{
				return BadRequest();
			}

			_context.Entry(tags).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TagsExists(id))
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

		// POST: api/Tags
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Tags>> PostTags(CreateTagRequest tagRequest)
		{
			if (tagRequest == null)
			{
				return BadRequest("No source");
			}
			Tags tag = new Tags { Name = tagRequest.Name, Description = tagRequest.Description };
			if (tagRequest.Image != null)
			{
				ISettingsResponse response = await UploadImage(tagRequest.Image, TagsFolder);
				if (response.IsSuccess)
				{
					tag.Image = response.SuccessMessage;
				}
			}
			_context.Tags.Add(tag);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetTags", new { id = tag.Id }, tag);
		}

		// DELETE: api/Tags/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTags(int id)
		{
			var tags = await _context.Tags.FindAsync(id);
			if (tags == null)
			{
				return NotFound();
			}

			_context.Tags.Remove(tags);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool TagsExists(int id)
		{
			return _context.Tags.Any(e => e.Id == id);
		}
	}
}