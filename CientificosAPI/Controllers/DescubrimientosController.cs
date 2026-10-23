using CientificosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CientificosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DescubrimientosController : ControllerBase
    {
        private readonly CientificosContext _context;

        public DescubrimientosController(CientificosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDescubrimientos()
        {
            var descubrimientos = await _context.Descubrimientos.ToListAsync();
            return Ok(descubrimientos);
        }

        [HttpGet("/api/cientificos/{cientificoId}/descubrimientos")]
        public async Task<IActionResult> GetDescubrimientosPorCientifico(int cientificoId)
        {
            var descubrimientos = await _context.Descubrimientos
                .Where(d => d.CientificoId == cientificoId)
                .ToListAsync();
            return Ok(descubrimientos);
        }

        [HttpPost]
        public async Task<IActionResult> PostDescubrimiento([FromBody] Descubrimiento descubrimiento)
        {
            _context.Descubrimientos.Add(descubrimiento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDescubrimientos), new { id = descubrimiento.Id }, descubrimiento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDescubrimiento(int id, [FromBody] Descubrimiento descubrimiento)
        {
            if (id != descubrimiento.Id) return BadRequest("El ID no coincide.");
            _context.Entry(descubrimiento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDescubrimiento(int id)
        {
            var descubrimiento = await _context.Descubrimientos.FindAsync(id);
            if (descubrimiento == null) return NotFound();
            _context.Descubrimientos.Remove(descubrimiento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}