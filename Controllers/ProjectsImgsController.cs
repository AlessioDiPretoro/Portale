using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portale.Data;
using Portale.Models;

namespace Portale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsImgsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectsImgsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProjectsImgs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectsImgs>>> GetProjectsImgs()
        {
            return await _context.ProjectsImgs.ToListAsync();
        }

        // GET: api/ProjectsImgs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectsImgs>> GetProjectsImgs(int id)
        {
            var projectsImgs = await _context.ProjectsImgs.FindAsync(id);

            if (projectsImgs == null)
            {
                return NotFound();
            }

            return projectsImgs;
        }

        // PUT: api/ProjectsImgs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectsImgs(int id, ProjectsImgs projectsImgs)
        {
            if (id != projectsImgs.Id)
            {
                return BadRequest();
            }

            _context.Entry(projectsImgs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectsImgsExists(id))
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

        // POST: api/ProjectsImgs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProjectsImgs>> PostProjectsImgs(ProjectsImgs projectsImgs)
        {
            _context.ProjectsImgs.Add(projectsImgs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectsImgs", new { id = projectsImgs.Id }, projectsImgs);
        }

        // DELETE: api/ProjectsImgs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectsImgs(int id)
        {
            var projectsImgs = await _context.ProjectsImgs.FindAsync(id);
            if (projectsImgs == null)
            {
                return NotFound();
            }

            _context.ProjectsImgs.Remove(projectsImgs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectsImgsExists(int id)
        {
            return _context.ProjectsImgs.Any(e => e.Id == id);
        }
    }
}
