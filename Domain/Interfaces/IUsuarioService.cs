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
        Task<Usuario> RegisterAsync(string nombre, string apellido, string email, string password, bool isAdminLoggedIn, string rango = "Cliente");
        Task<string?> LoginAsync(string email, string password);
        Task<List<Usuario>> ObtenerUsuarios();
        Task<Usuario> ObtenerUsuarioPorId(int id);
        Task AgregarUsuario(Usuario usuario);
        Task<int> ContarUsuariosClientes();
        Task ModificarUsuario(Usuario usuario);
        Task EliminarUsuario(int id);
        Task<Usuario?> ObtenerUsuarioPorEmail(string email);
    }
}