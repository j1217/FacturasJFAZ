namespace FacturasJFAZ.Models
{
    public class TipoCliente
    {
        public int Id { get; set; }
        public string tipoCliente { get; set; }

        // Propiedad de navegación para relacionar con los clientes
        public List<Cliente> Clientes { get; set; }
    }

}
