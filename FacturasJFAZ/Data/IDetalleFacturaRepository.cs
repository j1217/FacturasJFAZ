using FacturasJFAZ.Models;
using System;
using System.Collections.Generic;

namespace FacturasJFAZ.Data
{
    public interface IDetalleFacturaRepository
    {
        List<DetalleFactura> GetDetallesByFacturaId(int facturaId);
        void CreateDetalleFactura(DetalleFactura detalleFactura);
        void UpdateDetalleFactura(DetalleFactura detalleFactura);
        void DeleteDetalleFactura(int detalleFacturaId);
    }
}
