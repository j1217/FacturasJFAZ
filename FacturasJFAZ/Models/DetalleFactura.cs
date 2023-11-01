namespace FacturasJFAZ.Models
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public int CantidadDeProducto { get; set; }
        public int PrecioUnitarioProducto { get; set; }
        public decimal SubTotalProducto { get; set; }
        public string Nota { get; set; }

        // Propiedades de navegación para relacionar con la factura y el producto
        public Factura Factura { get; set; }
        public Producto Producto { get; set; }
    }

}
