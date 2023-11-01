using FacturasJFAZ.Models;
using System;
using System.Collections.Generic;

namespace FacturasJFAZ.Data
{
    public interface IClienteRepository
    {
        List<Cliente> GetAllClientes();
        Cliente GetClienteById(int clienteId);
        void CreateCliente(Cliente cliente);
        void UpdateCliente(Cliente cliente);
        void DeleteCliente(int clienteId);
    }
}
