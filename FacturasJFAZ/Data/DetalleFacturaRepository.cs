namespace FacturasJFAZ.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using FacturasJFAZ.Models;
    using Microsoft.Extensions.Configuration;

    public class DetalleFacturaRepository : IDetalleFacturaRepository
    {
        private readonly string _connectionString;

        public DetalleFacturaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<DetalleFactura> GetDetallesByFacturaId(int facturaId)
        {
            List<DetalleFactura> detalles = new List<DetalleFactura>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_GetDetallesByFacturaId @FacturaId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@FacturaId", SqlDbType.Int).Value = facturaId;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            detalles.Add(MapDetalleFacturaFromDataReader(reader));
                        }
                    }
                }
            }

            return detalles;
        }

        public void CreateDetalleFactura(DetalleFactura detalleFactura)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_CreateDetalleFactura @IdFactura, @IdProducto, @CantidadDeProducto, @PrecioUnitarioProducto, @SubTotalProducto, @Nota";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@IdFactura", SqlDbType.Int).Value = detalleFactura.IdFactura;
                    command.Parameters.Add("@IdProducto", SqlDbType.Int).Value = detalleFactura.IdProducto;
                    command.Parameters.Add("@CantidadDeProducto", SqlDbType.Int).Value = detalleFactura.CantidadDeProducto;
                    command.Parameters.Add("@PrecioUnitarioProducto", SqlDbType.Decimal).Value = detalleFactura.PrecioUnitarioProducto;
                    command.Parameters.Add("@SubTotalProducto", SqlDbType.Decimal).Value = detalleFactura.SubTotalProducto;
                    command.Parameters.Add("@Nota", SqlDbType.VarChar).Value = detalleFactura.Nota;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateDetalleFactura(DetalleFactura detalleFactura)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_UpdateDetalleFactura @Id, @IdFactura, @IdProducto, @CantidadDeProducto, @PrecioUnitarioProducto, @SubTotalProducto, @Nota";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = detalleFactura.Id;
                    command.Parameters.Add("@IdFactura", SqlDbType.Int).Value = detalleFactura.IdFactura;
                    command.Parameters.Add("@IdProducto", SqlDbType.Int).Value = detalleFactura.IdProducto;
                    command.Parameters.Add("@CantidadDeProducto", SqlDbType.Int).Value = detalleFactura.CantidadDeProducto;
                    command.Parameters.Add("@PrecioUnitarioProducto", SqlDbType.Decimal).Value = detalleFactura.PrecioUnitarioProducto;
                    command.Parameters.Add("@SubTotalProducto", SqlDbType.Decimal).Value = detalleFactura.SubTotalProducto;
                    command.Parameters.Add("@Nota", SqlDbType.VarChar).Value = detalleFactura.Nota;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDetalleFactura(int detalleFacturaId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_DeleteDetalleFactura @DetalleFacturaId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@DetalleFacturaId", SqlDbType.Int).Value = detalleFacturaId;
                    command.ExecuteNonQuery();
                }
            }
        }

        private DetalleFactura MapDetalleFacturaFromDataReader(SqlDataReader reader)
        {
            return new DetalleFactura
            {
                Id = (int)reader["Id"],
                IdFactura = (int)reader["IdFactura"],
                IdProducto = (int)reader["IdProducto"],
                CantidadDeProducto = (int)reader["CantidadDeProducto"],
                PrecioUnitarioProducto = (int)reader["PrecioUnitarioProducto"],
                SubTotalProducto = (decimal)reader["SubTotalProducto"],
                Nota = reader["Nota"].ToString()
            };
        }
    }

}
