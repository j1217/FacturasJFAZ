using Microsoft.AspNetCore.Mvc;
using FacturasJFAZ.Data;
using FacturasJFAZ.Models;
using FacturasJFAZ.Models.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;

public class FacturaController : Controller
{
    private readonly IFacturaRepository _facturaRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IProductoRepository _productoRepository;
    private readonly IDetalleFacturaRepository _detalleFacturaRepository;

    public FacturaController(IFacturaRepository facturaRepository, IClienteRepository clienteRepository, IProductoRepository productoRepository, IDetalleFacturaRepository detalleFacturaRepository)
    {
        _facturaRepository = facturaRepository;
        _clienteRepository = clienteRepository;
        _productoRepository = productoRepository;
        _detalleFacturaRepository = detalleFacturaRepository;
    }


    public IActionResult Index()
    {
        List<Factura> facturas = _facturaRepository.GetAllFacturas();
        return View(facturas);
    }

    public IActionResult Create()
    {
        // Aquí puedes obtener los datos necesarios para poblar las listas desplegables en tu vista
        var clientes = _clienteRepository.GetAllClientes();
        var productos = _productoRepository.GetAllProductos();
        // Puedes crear una ViewModel para pasar datos a la vista
        var viewModel = new FacturaViewModel
        {
            Clientes = clientes,
            Productos = productos
        };
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Create(FacturaViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Crear una nueva factura con los datos del ViewModel
            Factura nuevaFactura = model.Factura;
        
            // Llamar al método CreateFactura para guardar la factura en la base de datos
            _facturaRepository.CreateFactura(nuevaFactura);

            // Recorrer los productos en el ViewModel y crear detalles de factura
            foreach (var detalle in model.Detalles)
            {
                detalle.IdFactura = model.Factura.NumeroFactura; // Usar el ID de la nueva factura
                // Llamar al método CreateDetalleFactura para guardar el detalle en la base de datos
                _detalleFacturaRepository.CreateDetalleFactura(detalle);
            }

            // Redirigir a la vista de detalles de la nueva factura o a donde sea necesario
            return RedirectToAction("Details", new { id = model.Factura.NumeroFactura });
        }

        // Si la validación falla, regresar a la vista con el ViewModel para mostrar errores
        model.Clientes = _clienteRepository.GetAllClientes(); // Obtener la lista de clientes
        model.Productos = _productoRepository.GetAllProductos(); // Obtener la lista de productos
        return View(model);
    }
    public IActionResult Search(string numeroFactura, int idCliente)
    {
        List<Factura> facturas = _facturaRepository.SearchFacturas(numeroFactura, idCliente);
        return View("Index", facturas);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
