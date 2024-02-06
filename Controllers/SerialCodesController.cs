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
    public class SerialCodesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SerialCodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SerialCodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SerialCode>>> GetSerialCodes()
        {
            return await _context.SerialCodes.ToListAsync();
        }

        // GET: api/SerialCodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SerialCode>> GetSerialCode(int id)
        {
            var serialCode = await _context.SerialCodes.FindAsync(id);

            if (serialCode == null)
            {
                return NotFound();
            }

            return serialCode;
        }

        // PUT: api/SerialCodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSerialCode(int id, SerialCode serialCode)
        {
            if (id != serialCode.Id)
            {
                return BadRequest();
            }

            _context.Entry(serialCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SerialCodeExists(id))
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

        // POST: api/SerialCodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SerialCode>> PostSerialCode(SerialCode serialCode)
        {
            _context.SerialCodes.Add(serialCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSerialCode", new { id = serialCode.Id }, serialCode);
        }

        // DELETE: api/SerialCodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSerialCode(int id)
        {
            var serialCode = await _context.SerialCodes.FindAsync(id);
            if (serialCode == null)
            {
                return NotFound();
            }

            _context.SerialCodes.Remove(serialCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SerialCodeExists(int id)
        {
            return _context.SerialCodes.Any(e => e.Id == id);
        }
    }
}
