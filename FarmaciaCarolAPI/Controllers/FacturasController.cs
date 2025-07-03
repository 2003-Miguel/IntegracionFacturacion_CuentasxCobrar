using FarmaciaCarolAPI.Data;
using FarmaciaCarolAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FarmaciaCarolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public FacturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostFactura([FromBody] Factura model)
        {
            var ultimoID = _context.Facturas.Max(f => (int?)f.FacturaID) ?? 0;
            model.FacturaID = ultimoID + 1;

            model.Fecha = DateTime.Today;
            model.Hora = DateTime.Now.TimeOfDay;

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Facturas ON;");

                _context.Facturas.Add(model);
                await _context.SaveChangesAsync();

                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Facturas OFF;");

                await transaction.CommitAsync();

                return Ok("Factura registrada correctamente.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error al registrar factura: {ex.Message}");
            }
        }
        [HttpGet("cuentasporcobrar/{clienteID}")]
        public async Task<IActionResult> GetFacturasPorCliente(int clienteID)
        {
            var facturas = await _context.Facturas
                .Where(f => f.ClienteID == clienteID)
                .Include(f => f.Cliente)
                .Select(f => new
                {
                    facturaID = f.FacturaID,
                    fecha = f.Fecha.ToString("yyyy-MM-dd"),
                    hora = f.Hora.ToString(@"hh\:mm\:ss"),
                    monto = f.Total,
                    estado = "Pendiente",
                    clienteId = f.ClienteID,
                })
                .ToListAsync();

            decimal saldoPendiente = facturas.Sum(f => f.estado == "Pendiente" ? f.monto : 0);

            return Ok(new
            {
                saldoPendiente,
                facturas
            });
        }
    }
}
