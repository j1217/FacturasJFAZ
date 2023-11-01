namespace FacturasJFAZ.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using FacturasJFAZ.Models;
    using Microsoft.Extensions.Configuration;

    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString;

        public ProductoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Producto> GetAllProductos()
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_GetAllProductos";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productos.Add(MapProductoFromDataReader(reader));
                        }
                    }
                }
            }

            return productos;
        }

        public Producto GetProductoById(int productoId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_GetProductoById @ProductoId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = productoId;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapProductoFromDataReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public void CreateProducto(Producto producto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_CreateProducto @NombreProducto, @ImagenProducto, @PrecioUnitario, @Ext";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@NombreProducto", SqlDbType.VarChar).Value = producto.NombreProducto;
                    command.Parameters.Add("@ImagenProducto", SqlDbType.VarBinary).Value = producto.ImagenProducto;
                    command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = producto.PrecioUnitario;
                    command.Parameters.Add("@Ext", SqlDbType.VarChar).Value = producto.Ext;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProducto(Producto producto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_UpdateProducto @Id, @NombreProducto, @ImagenProducto, @PrecioUnitario, @Ext";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = producto.Id;
                    command.Parameters.Add("@NombreProducto", SqlDbType.VarChar).Value = producto.NombreProducto;
                    command.Parameters.Add("@ImagenProducto", SqlDbType.VarBinary).Value = producto.ImagenProducto;
                    command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = producto.PrecioUnitario;
                    command.Parameters.Add("@Ext", SqlDbType.VarChar).Value = producto.Ext;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProducto(int productoId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_DeleteProducto @ProductoId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = productoId;
                    command.ExecuteNonQuery();
                }
            }
        }

        private Producto MapProductoFromDataReader(SqlDataReader reader)
        {
            return new Producto
            {
                Id = (int)reader["Id"],
                NombreProducto = reader["NombreProducto"].ToString(),
                ImagenProducto = (byte[])reader["ImagenProducto"],
                PrecioUnitario = (decimal)reader["PrecioUnitario"],
                Ext = reader["Ext"].ToString()
            };
        }
    }

}
