using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Usuario>> ObtenerUsuarios()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado.");
            }
            return usuario; // Ahora es seguro devolver el usuario
        }

        public async Task AgregarUsuario(Usuario usuario)
        {
            // Asignar automáticamente a Cliente si no es Admin o Vendedor
            if (string.IsNullOrEmpty(usuario.Rango) || usuario.Rango != "Admin" && usuario.Rango != "Vendedor")
            {
                usuario.Rango = "Cliente";
            }
            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task<int> ContarUsuariosClientes()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Count(u => u.Rango == "Cliente");
        }

        public async Task ModificarUsuario(Usuario usuario)
        {
            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task EliminarUsuario(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }
    }
}