using FacturasJFAZ.Models;
using System;
using System.Collections.Generic;

namespace FacturasJFAZ.Data
{
    public interface IProductoRepository
    {
        List<Producto> GetAllProductos();
        Producto GetProductoById(int productoId);
        void CreateProducto(Producto producto);
        void UpdateProducto(Producto producto);
        void DeleteProducto(int productoId);
    }
}
