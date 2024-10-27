using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiEcommerce2.DTOs;
using Application.Dtos;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IJwtTokenService jwtTokenService)
        {
            _usuarioRepository = usuarioRepository;
            _jwtTokenService = jwtTokenService;
        }

        // Método para registrar un nuevo usuario
        public async Task<Usuario> RegisterAsync(string nombre, string apellido, string email, string password, bool isAdminLoggedIn, string rango = "Cliente")
        {
            rango = isAdminLoggedIn && !string.IsNullOrEmpty(rango) ? rango : "Cliente";
            var usuario = new Usuario
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password), // Asegúrate de que esta línea se ejecute correctamente
                Role = rango
            };

            await _usuarioRepository.AddAsync(usuario);
            return usuario;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var usuario = (await _usuarioRepository.GetAllAsync())
                .SingleOrDefault(u => u.Email == email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(password, usuario.Password))
            {
                return null; // O bien lanza una excepción, si deseas manejarla en el controlador
            }

            return _jwtTokenService.GenerateToken(usuario);
        }
        public async Task<List<Usuario>> ObtenerUsuarios()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado.");
            return usuario;
        }

        public async Task AgregarUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Role) || (usuario.Role != "Admin" && usuario.Role != "Vendedor"))
            {
                usuario.Role = "Cliente";
            }
            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task<int> ContarUsuariosClientes()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Count(u => u.Role == "Cliente");
        }

        public async Task ModificarUsuario(Usuario usuario)
        {
            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task EliminarUsuario(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<Usuario?> ObtenerUsuarioPorEmail(string email)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.FirstOrDefault(u => u.Email == email);
        }
    }
}