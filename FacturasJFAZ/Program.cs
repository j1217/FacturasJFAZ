using FacturasJFAZ.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "buscafactura",
    pattern: "Factura/BuscaFactura",
    defaults: new { controller = "Factura", action = "BuscaFactura" });

app.MapControllerRoute(
    name: "crearfactura",
    pattern: "Factura/CrearFactura",
    defaults: new { controller = "Factura", action = "CrearFactura" });

app.Run();
