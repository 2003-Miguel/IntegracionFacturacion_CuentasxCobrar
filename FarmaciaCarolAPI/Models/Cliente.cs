namespace FarmaciaCarolAPI.Models
{
    public class Cliente
    {
        public int ClienteID { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public ICollection<CuentaPorCobrar> CuentaPorCobrar { get; set; }
        public ICollection<Factura> Factura { get; set; }
    }
}
