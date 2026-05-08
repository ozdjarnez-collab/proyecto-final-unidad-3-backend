using BackendAPI.Data;
using BackendAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiciosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServiciosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicio>>> GetServicios()
        {
            return await _context.Servicios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Servicio>> GetServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio == null)
            {
                return NotFound("Servicio no encontrado.");
            }

            return servicio;
        }

        [HttpPost]
        public async Task<ActionResult<Servicio>> CreateServicio(Servicio servicio)
        {
            if (string.IsNullOrWhiteSpace(servicio.Nombre))
            {
                return BadRequest("El nombre del servicio es obligatorio.");
            }

            if (servicio.Precio <= 0)
            {
                return BadRequest("El precio debe ser mayor a 0.");
            }

            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServicio), new { id = servicio.Id }, servicio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServicio(int id, Servicio servicio)
        {
            if (id != servicio.Id)
            {
                return BadRequest("El id no coincide.");
            }

            if (string.IsNullOrWhiteSpace(servicio.Nombre))
            {
                return BadRequest("El nombre del servicio es obligatorio.");
            }

            if (servicio.Precio <= 0)
            {
                return BadRequest("El precio debe ser mayor a 0.");
            }

            _context.Entry(servicio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);

            if (servicio == null)
            {
                return NotFound("Servicio no encontrado.");
            }

            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}