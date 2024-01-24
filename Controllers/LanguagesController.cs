﻿using System;
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
    public class LanguagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LanguagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Languages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Languages>>> GetLanguages()
        {
            return await _context.Languages.ToListAsync();
        }

        // GET: api/Languages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Languages>> GetLanguages(int id)
        {
            var languages = await _context.Languages.FindAsync(id);

            if (languages == null)
            {
                return NotFound();
            }

            return languages;
        }

        // PUT: api/Languages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguages(int id, Languages languages)
        {
            if (id != languages.Id)
            {
                return BadRequest();
            }

            _context.Entry(languages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguagesExists(id))
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

        // POST: api/Languages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Languages>> PostLanguages(Languages languages)
        {
            _context.Languages.Add(languages);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLanguages", new { id = languages.Id }, languages);
        }

        // DELETE: api/Languages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguages(int id)
        {
            var languages = await _context.Languages.FindAsync(id);
            if (languages == null)
            {
                return NotFound();
            }

            _context.Languages.Remove(languages);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LanguagesExists(int id)
        {
            return _context.Languages.Any(e => e.Id == id);
        }
    }
}
