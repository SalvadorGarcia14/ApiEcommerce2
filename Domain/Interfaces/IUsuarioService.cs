using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> RegisterAsync(string nombre, string apellido, string email, string password, string role, bool isAdminLoggedIn);
        Task<Usuario?> ObtenerPorEmailAsync(string email);
        Task<string?> LoginAsync(string email, string password);
        Task<List<Usuario>> ObtenerUsuarios();
        Task<Usuario?> ObtenerUsuarioPorNombre(string nombre);
        Task AgregarUsuario(Usuario usuario);
        Task<int> ContarUsuariosClientes();
        Task ModificarUsuario(Usuario usuario);
        Task EliminarUsuarioPorNombre(string nombre);
        Task<Usuario?> ObtenerUsuarioPorEmail(string email);
        Task EliminarUsuarioPorEmail(string email);


    }
}