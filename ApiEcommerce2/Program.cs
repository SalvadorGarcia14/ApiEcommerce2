using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Application.Service; // Asegúrate de incluir este espacio de nombres

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(
    builder.Configuration["ConnectionStrings:DBConnectionString"], b => b.MigrationsAssembly("ApiEcommerce2")));

// Registro de repositorios y servicios
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();