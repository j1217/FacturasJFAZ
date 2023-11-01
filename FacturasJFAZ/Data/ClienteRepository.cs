namespace FacturasJFAZ.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using FacturasJFAZ.Models;
    using Microsoft.Extensions.Configuration;

    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Cliente> GetAllClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_GetAllClientes";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientes.Add(MapClienteFromDataReader(reader));
                        }
                    }
                }
            }

            return clientes;
        }

        public Cliente GetClienteById(int clienteId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_GetClienteById @ClienteId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ClienteId", SqlDbType.Int).Value = clienteId;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapClienteFromDataReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        public void CreateCliente(Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_CreateCliente @RazonSocial, @IdTipoCliente, @FechaCreacion, @RFC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@RazonSocial", SqlDbType.VarChar).Value = cliente.RazonSocial;
                    command.Parameters.Add("@IdTipoCliente", SqlDbType.Int).Value = cliente.IdTipoCliente;
                    command.Parameters.Add("@FechaCreacion", SqlDbType.DateTime).Value = cliente.FechaCreacion;
                    command.Parameters.Add("@RFC", SqlDbType.VarChar).Value = cliente.RFC;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCliente(Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_UpdateCliente @Id, @RazonSocial, @IdTipoCliente, @FechaCreacion, @RFC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = cliente.Id;
                    command.Parameters.Add("@RazonSocial", SqlDbType.VarChar).Value = cliente.RazonSocial;
                    command.Parameters.Add("@IdTipoCliente", SqlDbType.Int).Value = cliente.IdTipoCliente;
                    command.Parameters.Add("@FechaCreacion", SqlDbType.DateTime).Value = cliente.FechaCreacion;
                    command.Parameters.Add("@RFC", SqlDbType.VarChar).Value = cliente.RFC;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCliente(int clienteId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "EXEC sp_DeleteCliente @ClienteId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ClienteId", SqlDbType.Int).Value = clienteId;
                    command.ExecuteNonQuery();
                }
            }
        }

        private Cliente MapClienteFromDataReader(SqlDataReader reader)
        {
            return new Cliente
            {
                Id = (int)reader["Id"],
                RazonSocial = reader["RazonSocial"].ToString(),
                IdTipoCliente = (int)reader["IdTipoCliente"],
                FechaCreacion = (DateTime)reader["FechaCreacion"],
                RFC = reader["RFC"].ToString()
            };
        }
    }

}
