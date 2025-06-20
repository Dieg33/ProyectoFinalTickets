using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProyectoFinalTickets.Services;
using ProyectoFinalTickets.Data;

var builder = WebApplication.CreateBuilder(args);


// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddScoped<CorreoService>();


// Configuraci�n del DbContext y la cadena de conexi�n
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Crear la aplicaci�n
var app = builder.Build();

// Configurar la tuber�a de solicitud HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
   pattern: "{controller=Login}/{action=Index}/{id?}");


app.Run();
