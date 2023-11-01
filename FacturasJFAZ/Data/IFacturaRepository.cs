using FacturasJFAZ.Models;
using System;
using System.Collections.Generic;

namespace FacturasJFAZ.Data
{
    public interface IFacturaRepository
    {
        List<Factura> GetAllFacturas();
        Factura GetFacturaById(int facturaId);
        void CreateFactura(Factura factura);
        void UpdateFactura(Factura factura);
        void DeleteFactura(int facturaId);
        List<Factura> SearchFacturas(string numeroFactura, int idCliente);
    }
}
