using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiProject.Data;
using ApiProject.Model;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanalsController : ControllerBase
    {
        private readonly ApiProjectContext _context;

        public CanalsController(ApiProjectContext context)
        {
            _context = context;
        }

        // GET: api/Canals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Canal>>> GetCanals()
        {
            return await _context.Canal.ToListAsync();
        }

        // GET: api/Canals/5
        [HttpGet("{group}")]
        public async Task<ActionResult<IEnumerable<Canal>>> GetCanal(string group)
        {
            // Usar LINQ para filtrar los canales por el grupo especificado
            var canales = await _context.Canal.Where(c => c.group_title == group).ToListAsync();

            if (canales == null || !canales.Any())
            {
                return NotFound();
            }

            return Ok(canales);
        }

        // POST: api/Canals
        [HttpPost]
        public async Task<ActionResult<Canal>> PostCanal(Canal canal)
        {
            _context.Canal.Add(canal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCanal", new { id = canal.Id }, canal);
        }

        // PUT: api/Canals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanal(int id, Canal canal)
        {
            if (id != canal.Id)
            {
                return BadRequest();
            }

            _context.Entry(canal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanalExists(id))
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

        // DELETE: api/Canals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCanal(int id)
        {
            var canal = await _context.Canal.FindAsync(id);
            if (canal == null)
            {
                return NotFound();
            }

            _context.Canal.Remove(canal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CanalExists(int id)
        {
            return _context.Canal.Any(e => e.Id == id);
        }
    }
}
