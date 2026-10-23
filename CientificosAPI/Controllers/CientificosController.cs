using CientificosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CientificosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CientificosController : ControllerBase
    {
        private readonly CientificosContext _context;

        public CientificosController(CientificosContext context)
        {
            _context = context;
        }

        // GET: api/Cientificos
        [HttpGet]
        public async Task<IActionResult> GetCientificos()
        {
            var cientificos = await _context.Cientificos
                .Include(c => c.Especialidads)
                .ToListAsync();

            return Ok(cientificos);
        }

        // GET: api/Cientificos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCientifico(int id)
        {
            var cientifico = await _context.Cientificos
                .Include(c => c.Especialidads)
                .Include(c => c.Descubrimientos)
                .Include(c => c.Investigaciones)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cientifico == null) return NotFound();
            return Ok(cientifico);
        }

        // POST: api/Cientificos
        [HttpPost]
        public async Task<IActionResult> PostCientifico([FromBody] Cientifico cientifico)
        {
            _context.Cientificos.Add(cientifico);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCientifico), new { id = cientifico.Id }, cientifico);
        }

        // PUT: api/Cientificos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCientifico(int id, [FromBody] Cientifico cientifico)
        {
            if (id != cientifico.Id) return BadRequest("El ID no coincide.");

            _context.Entry(cientifico).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Cientificos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCientifico(int id)
        {
            var cientifico = await _context.Cientificos.FindAsync(id);
            if (cientifico == null) return NotFound();

            _context.Cientificos.Remove(cientifico);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}