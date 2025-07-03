using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmaciaCarolAPI.Models
{
    public class CuentaPorCobrar
    {
        [Key]
        public int CxCID { get; set; }
        public int ClienteID { get; set; }
        public int FacturaID { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        [ForeignKey("ClienteID")]
        public Cliente Cliente { get; set; }
    }
}
