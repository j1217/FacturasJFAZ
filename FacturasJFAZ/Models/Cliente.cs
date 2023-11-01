namespace FacturasJFAZ.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public int IdTipoCliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string RFC { get; set; }

        // Propiedad de navegación para relacionar con el tipo de cliente
        public TipoCliente TipoCliente { get; set; }
    }

}
