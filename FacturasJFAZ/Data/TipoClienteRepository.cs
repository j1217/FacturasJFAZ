namespace FacturasJFAZ.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using FacturasJFAZ.Models;
    using Microsoft.Extensions.Configuration;

    public class TipoClienteRepository : ITipoClienteRepository
    {
        private readonly string _connectionString;

        public TipoClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<TipoCliente> GetAllTipoClientes()
        {
            List<TipoCliente> tipoClientes = new List<TipoCliente>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_GetAllTipoClientes";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tipoClientes.Add(MapTipoClienteFromDataReader(reader));
                        }
                    }
                }
            }

            return tipoClientes;
        }

        public TipoCliente GetTipoClienteById(int tipoClienteId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_GetTipoClienteById @TipoClienteId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@TipoClienteId", SqlDbType.Int).Value = tipoClienteId;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapTipoClienteFromDataReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public void CreateTipoCliente(TipoCliente tipoCliente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_CreateTipoCliente @TipoCliente";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@TipoCliente", SqlDbType.VarChar).Value = tipoCliente.tipoCliente;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTipoCliente(TipoCliente tipoCliente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_UpdateTipoCliente @Id, @TipoCliente";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = tipoCliente.Id;
                    command.Parameters.Add("@TipoCliente", SqlDbType.VarChar).Value = tipoCliente.tipoCliente;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTipoCliente(int tipoClienteId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_DeleteTipoCliente @TipoClienteId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@TipoClienteId", SqlDbType.Int).Value = tipoClienteId;
                    command.ExecuteNonQuery();
                }
            }
        }

        private TipoCliente MapTipoClienteFromDataReader(SqlDataReader reader)
        {
            return new TipoCliente
            {
                Id = (int)reader["Id"],
                tipoCliente = reader["TipoCliente"].ToString()
            };
        }
    }

}
