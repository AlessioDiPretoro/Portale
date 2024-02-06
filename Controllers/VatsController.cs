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
    public class VatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Vats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vat>>> GetVats()
        {
            return await _context.Vats.ToListAsync();
        }

        // GET: api/Vats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vat>> GetVat(int id)
        {
            var vat = await _context.Vats.FindAsync(id);

            if (vat == null)
            {
                return NotFound();
            }

            return vat;
        }

        // PUT: api/Vats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVat(int id, Vat vat)
        {
            if (id != vat.Id)
            {
                return BadRequest();
            }

            _context.Entry(vat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VatExists(id))
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

        // POST: api/Vats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vat>> PostVat(Vat vat)
        {
            _context.Vats.Add(vat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVat", new { id = vat.Id }, vat);
        }

        // DELETE: api/Vats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVat(int id)
        {
            var vat = await _context.Vats.FindAsync(id);
            if (vat == null)
            {
                return NotFound();
            }

            _context.Vats.Remove(vat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VatExists(int id)
        {
            return _context.Vats.Any(e => e.Id == id);
        }
    }
}
