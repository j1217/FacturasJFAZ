namespace FacturasJFAZ.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public byte[] ImagenProducto { get; set; } 
        public decimal PrecioUnitario { get; set; }
        public string Ext { get; set; }

        // Propiedades de navegación para relacionar con los detalles de factura
        public List<DetalleFactura> DetallesFactura { get; set; }
    }

}
