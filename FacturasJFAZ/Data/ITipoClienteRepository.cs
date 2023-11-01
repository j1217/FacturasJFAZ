using FacturasJFAZ.Models;
using System;
using System.Collections.Generic;

namespace FacturasJFAZ.Data
{
    public interface ITipoClienteRepository
    {
        List<TipoCliente> GetAllTipoClientes();
        TipoCliente GetTipoClienteById(int tipoClienteId);
        void CreateTipoCliente(TipoCliente tipoCliente);
        void UpdateTipoCliente(TipoCliente tipoCliente);
        void DeleteTipoCliente(int tipoClienteId);
    }
}
