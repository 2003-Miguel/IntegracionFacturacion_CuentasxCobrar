using Microsoft.EntityFrameworkCore;
using FarmaciaCarolAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace FarmaciaCarolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasPorCobrarController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CuentasPorCobrarController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{clienteId}")]
        public async Task<IActionResult> GetCxC(int clienteId)
        {
            var cuentas = await _context.CuentasPorCobrar
                .Where(c => c.ClienteID == clienteId)
                .ToListAsync();

            if (!cuentas.Any())
                return NotFound("No hay cuentas por cobrar para este cliente.");

            var total = cuentas.Sum(c => c.Monto);
            return Ok(new { clienteId, saldoPendiente = total, facturas = cuentas });
        }

        [HttpGet]
        public async Task<IActionResult> GetTodasCxC()
        {
            var cuentasConClientes = await _context.CuentasPorCobrar
                .Include(c => c.Cliente)
                .Select(cxc => new
                {
                    cxc.CxCID,
                    cxc.FacturaID,
                    cxc.ClienteID,
                    clienteNombre = cxc.Cliente.Nombre,
                    Fecha = cxc.Fecha.ToString("yyyy-MM-dd"),
                    cxc.Hora,
                    cxc.Monto,
                    cxc.Estado
                })
                .ToListAsync();

            return Ok(cuentasConClientes);
        }
    }
}
