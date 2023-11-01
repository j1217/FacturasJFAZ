namespace FacturasJFAZ.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using FacturasJFAZ.Models;
    using Microsoft.Extensions.Configuration;

    public class FacturaRepository : IFacturaRepository
    {
        private readonly string _connectionString;

        public FacturaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Factura> GetAllFacturas()
        {
            List<Factura> facturas = new List<Factura>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_GetAllFacturas";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            facturas.Add(MapFacturaFromDataReader(reader));
                        }
                    }
                }
            }

            return facturas;
        }

        public Factura GetFacturaById(int facturaId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_GetFacturaById @FacturaId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@FacturaId", SqlDbType.Int).Value = facturaId;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapFacturaFromDataReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public void CreateFactura(Factura factura)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_CreateFactura @FechaEmisionFactura, @IdCliente, @NumeroFactura, @NumeroTotalArticulos, @SubTotalFactura, @TotalImpuestos, @TotalFactura";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@FechaEmisionFactura", SqlDbType.DateTime).Value = factura.FechaEmisionFactura;
                    command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = factura.IdCliente;
                    command.Parameters.Add("@NumeroFactura", SqlDbType.Int).Value = factura.NumeroFactura;
                    command.Parameters.Add("@NumeroTotalArticulos", SqlDbType.Int).Value = factura.NumeroTotalArticulos;
                    command.Parameters.Add("@SubTotalFactura", SqlDbType.Decimal).Value = factura.SubTotalFactura;
                    command.Parameters.Add("@TotalImpuestos", SqlDbType.Decimal).Value = factura.TotalImpuestos;
                    command.Parameters.Add("@TotalFactura", SqlDbType.Decimal).Value = factura.TotalFactura;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateFactura(Factura factura)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_UpdateFactura @Id, @FechaEmisionFactura, @IdCliente, @NumeroFactura, @NumeroTotalArticulos, @SubTotalFactura, @TotalImpuestos, @TotalFactura";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = factura.Id;
                    command.Parameters.Add("@FechaEmisionFactura", SqlDbType.DateTime).Value = factura.FechaEmisionFactura;
                    command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = factura.IdCliente;
                    command.Parameters.Add("@NumeroFactura", SqlDbType.Int).Value = factura.NumeroFactura;
                    command.Parameters.Add("@NumeroTotalArticulos", SqlDbType.Int).Value = factura.NumeroTotalArticulos;
                    command.Parameters.Add("@SubTotalFactura", SqlDbType.Decimal).Value = factura.SubTotalFactura;
                    command.Parameters.Add("@TotalImpuestos", SqlDbType.Decimal).Value = factura.TotalImpuestos;
                    command.Parameters.Add("@TotalFactura", SqlDbType.Decimal).Value = factura.TotalFactura;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteFactura(int facturaId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_DeleteFactura @FacturaId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@FacturaId", SqlDbType.Int).Value = facturaId;
                    command.ExecuteNonQuery();
                }
            }
        }

        private Factura MapFacturaFromDataReader(SqlDataReader reader)
        {
            return new Factura
            {
                Id = (int)reader["Id"],
                FechaEmisionFactura = (DateTime)reader["FechaEmisionFactura"],
                IdCliente = (int)reader["IdCliente"],
                NumeroFactura = (int)reader["NumeroFactura"],
                NumeroTotalArticulos = (int)reader["NumeroTotalArticulos"],
                SubTotalFactura = (decimal)reader["SubTotalFactura"],
                TotalImpuestos = (decimal)reader["TotalImpuestos"],
                TotalFactura = (decimal)reader["TotalFactura"]
            };
        }

        public List<Factura> SearchFacturas(string numeroFactura, int idCliente)
        {
            List<Factura> facturas = new List<Factura>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_SearchFacturas @NumeroFactura, @IdCliente";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@NumeroFactura", SqlDbType.VarChar).Value = numeroFactura;
                    command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            facturas.Add(MapFacturaFromDataReader(reader));
                        }
                    }
                }
            }

            return facturas;
        }

    }

}
