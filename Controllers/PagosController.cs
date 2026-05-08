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
    public class PagosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PagosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            return await _context.Pagos
                .Include(p => p.Reserva)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _context.Pagos
                .Include(p => p.Reserva)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pago == null)
            {
                return NotFound("Pago no encontrado.");
            }

            return pago;
        }

        [HttpPost]
        public async Task<ActionResult<Pago>> CreatePago(Pago pago)
        {
            var reservaExiste = await _context.Reservas.AnyAsync(r => r.Id == pago.ReservaId);

            if (!reservaExiste)
            {
                return BadRequest("La reserva no existe.");
            }

            var pagoExiste = await _context.Pagos.AnyAsync(p => p.ReservaId == pago.ReservaId);

            if (pagoExiste)
            {
                return BadRequest("Esta reserva ya tiene un pago registrado.");
            }

            if (pago.Monto <= 0)
            {
                return BadRequest("El monto debe ser mayor a 0.");
            }

            if (pago.FechaPago == default)
            {
                pago.FechaPago = DateTime.Now;
            }

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPago), new { id = pago.Id }, pago);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePago(int id, Pago pago)
        {
            if (id != pago.Id)
            {
                return BadRequest("El id no coincide.");
            }

            var existePago = await _context.Pagos.AnyAsync(p => p.Id == id);

            if (!existePago)
            {
                return NotFound("Pago no encontrado.");
            }

            _context.Entry(pago).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);

            if (pago == null)
            {
                return NotFound("Pago no encontrado.");
            }

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}