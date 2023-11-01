namespace FacturasJFAZ.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime FechaEmisionFactura { get; set; }
        public int IdCliente { get; set; }
        public int NumeroFactura { get; set; }
        public int NumeroTotalArticulos { get; set; }
        public decimal SubTotalFactura { get; set; }
        public decimal TotalImpuestos { get; set; }
        public decimal TotalFactura { get; set; }

        // Propiedades de navegación para relacionar con el cliente y los detalles de factura
        public Cliente Cliente { get; set; }
        public List<DetalleFactura> DetallesFactura { get; set; }
    }

}
