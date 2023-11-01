using System;
using System.Collections.Generic;
using FacturasJFAZ.Models;

namespace FacturasJFAZ.Models.ViewModels
{
    public class FacturaViewModel
    {
        public Factura Factura { get; set; }
        public List<Cliente> Clientes { get; set; }
        public List<Producto> Productos { get; set; }
        public List<DetalleFactura> Detalles { get; set; }
        public List<Factura> Facturas { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
        public string NumeroFactura { get; set; }
        public int IdCliente { get; set; }
        public int Id { get; set; }
    }
}
