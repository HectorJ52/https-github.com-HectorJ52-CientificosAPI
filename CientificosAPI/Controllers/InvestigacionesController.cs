using CientificosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CientificosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestigacionesController : ControllerBase
    {
        private readonly CientificosContext _context;

        public InvestigacionesController(CientificosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvestigaciones()
        {
            var investigaciones = await _context.Investigaciones.ToListAsync();
            return Ok(investigaciones);
        }

        [HttpGet("estado/{estado}")]
        public async Task<IActionResult> GetInvestigacionesPorEstado(string estado)
        {
            var investigaciones = await _context.Investigaciones
                .Where(i => i.Estado == estado)
                .ToListAsync();
            return Ok(investigaciones);
        }

        [HttpPost]
        public async Task<IActionResult> PostInvestigacion([FromBody] Investigacione investigacion)
        {
            _context.Investigaciones.Add(investigacion);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetInvestigaciones), new { id = investigacion.Id }, investigacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestigacion(int id, [FromBody] Investigacione investigacion)
        {
            if (id != investigacion.Id) return BadRequest("El ID no coincide.");
            _context.Entry(investigacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestigacion(int id)
        {
            var investigacion = await _context.Investigaciones.FindAsync(id);
            if (investigacion == null) return NotFound();
            _context.Investigaciones.Remove(investigacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}