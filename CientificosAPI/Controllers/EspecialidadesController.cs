using CientificosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CientificosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadesController : ControllerBase
    {
        private readonly CientificosContext _context;

        public EspecialidadesController(CientificosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEspecialidades()
        {
            var especialidades = await _context.Especialidades.ToListAsync();
            return Ok(especialidades);
        }

        [HttpPost]
        public async Task<IActionResult> PostEspecialidad([FromBody] Especialidade especialidad)
        {
            _context.Especialidades.Add(especialidad);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEspecialidades), new { id = especialidad.Id }, especialidad);
        }
    }
}