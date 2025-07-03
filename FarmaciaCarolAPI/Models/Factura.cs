using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FarmaciaCarolAPI.Models
{
    public class Factura
    {
        public int FacturaID { get; set; }
        [Required]
        public int ClienteID { get; set; }
        public DateTime Fecha { get; set; }
        [Required]
        public TimeSpan Hora { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public virtual Cliente? Cliente { get; set; }
    }
    
}
