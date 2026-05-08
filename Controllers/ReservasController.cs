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
    public class ReservasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Servicio)
                .Include(r => r.Pago)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Servicio)
                .Include(r => r.Pago)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null)
            {
                return NotFound("Reserva no encontrada.");
            }

            return reserva;
        }

        [HttpPost]
        public async Task<ActionResult<Reserva>> CreateReserva(Reserva reserva)
        {
            var clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == reserva.ClienteId);
            var servicioExiste = await _context.Servicios.AnyAsync(s => s.Id == reserva.ServicioId);

            if (!clienteExiste)
            {
                return BadRequest("El cliente no existe.");
            }

            if (!servicioExiste)
            {
                return BadRequest("El servicio no existe.");
            }

            if (reserva.FechaReserva == default)
            {
                reserva.FechaReserva = DateTime.Now;
            }

            if (string.IsNullOrWhiteSpace(reserva.Estado))
            {
                reserva.Estado = "Pendiente";
            }

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReserva), new { id = reserva.Id }, reserva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReserva(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest("El id no coincide.");
            }

            var existeReserva = await _context.Reservas.AnyAsync(r => r.Id == id);

            if (!existeReserva)
            {
                return NotFound("Reserva no encontrada.");
            }

            _context.Entry(reserva).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound("Reserva no encontrada.");
            }

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}