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
        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = (await _usuarioRepository.GetAllAsync())
                .SingleOrDefault(u => u.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            return _jwtTokenService.GenerateToken(user);
        }



        public async Task<Usuario> RegisterAsync(string nombre, string apellido, string email, string password, string role, bool isAdminLoggedIn)
        {
            role = isAdminLoggedIn && !string.IsNullOrEmpty(role) ? role : "Cliente";

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); // Generar el hash

            var usuario = new Usuario
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Password = hashedPassword, // Almacena el hash en vez de la contraseña
                Role = role
            };

            await _usuarioRepository.AddAsync(usuario);
            return usuario;
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string email)
        {
            return await _usuarioRepository.GetByEmailAsync(email);
        }
        public async Task<List<Usuario>> ObtenerUsuarios()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario?> ObtenerUsuarioPorNombre(string nombre)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.FirstOrDefault(u => u.Nombre == nombre);
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

        public async Task EliminarUsuarioPorNombre(string nombre)
        {
            var usuario = await ObtenerUsuarioPorNombre(nombre);
            if (usuario != null)
            {
                await _usuarioRepository.DeleteAsync(usuario.Id);  // Usar ID para eliminación
            }
        }

        public async Task<Usuario?> ObtenerUsuarioPorEmail(string email)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.FirstOrDefault(u => u.Email == email);
        }
    }
}