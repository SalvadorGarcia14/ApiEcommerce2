﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Entities;


namespace Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> GetAllAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> GetByNombreAsync(string nombre)
        {
            var producto = await _context.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Nombre == nombre);

            if (producto == null)
            {
                throw new InvalidOperationException("Producto no encontrado.");
            }

            return producto;
        }

        public async Task<Producto> GetByIdAsync(int id)
        {
            var producto = await _context.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
            {
                throw new InvalidOperationException("Producto no encontrado.");
            }

            return producto;
        }

        public async Task AddAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrUpdateAsync(Producto producto)
        {
            var productoExistente = await _context.Productos
                .FirstOrDefaultAsync(p => p.Nombre == producto.Nombre && p.Categoria == producto.Categoria);

            if (productoExistente != null)
            {
                // Si el producto ya existe, sumamos la cantidad y actualizamos el precio
                productoExistente.Cantidad += producto.Cantidad;
                productoExistente.Precio = producto.Precio;
                productoExistente.Imagen = producto.Imagen;
                productoExistente.Descripcion = producto.Descripcion; 

                _context.Productos.Update(productoExistente);
            }
            else
            {
                // Si no existe, agregamos el nuevo producto
                await _context.Productos.AddAsync(producto);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Producto producto)
        {
            var productoExistente = await _context.Productos.FindAsync(producto.Id);

            if (productoExistente == null)
            {
                throw new InvalidOperationException("Producto no encontrado.");
            }

            // Actualiza los valores del producto existente con los del nuevo producto
            _context.Entry(productoExistente).CurrentValues.SetValues(producto);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                throw new InvalidOperationException("Producto no encontrado.");
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }

    }
}


