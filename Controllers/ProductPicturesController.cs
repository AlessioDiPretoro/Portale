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
    public class ProductPicturesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductPicturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductPictures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPicture>>> GetProductPictures()
        {
            return await _context.ProductPictures.ToListAsync();
        }

        // GET: api/ProductPictures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductPicture>> GetProductPicture(int id)
        {
            var productPicture = await _context.ProductPictures.FindAsync(id);

            if (productPicture == null)
            {
                return NotFound();
            }

            return productPicture;
        }

        // PUT: api/ProductPictures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductPicture(int id, ProductPicture productPicture)
        {
            if (id != productPicture.Id)
            {
                return BadRequest();
            }

            _context.Entry(productPicture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPictureExists(id))
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

        // POST: api/ProductPictures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductPicture>> PostProductPicture(ProductPicture productPicture)
        {
            _context.ProductPictures.Add(productPicture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductPicture", new { id = productPicture.Id }, productPicture);
        }

        // DELETE: api/ProductPictures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductPicture(int id)
        {
            var productPicture = await _context.ProductPictures.FindAsync(id);
            if (productPicture == null)
            {
                return NotFound();
            }

            _context.ProductPictures.Remove(productPicture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductPictureExists(int id)
        {
            return _context.ProductPictures.Any(e => e.Id == id);
        }
    }
}
