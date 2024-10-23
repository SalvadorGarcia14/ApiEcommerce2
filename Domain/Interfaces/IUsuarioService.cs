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
        Task<List<Usuario>> ObtenerUsuarios();
        Task<Usuario> ObtenerUsuarioPorId(int id);
        Task AgregarUsuario(Usuario usuario);
        Task ModificarUsuario(Usuario usuario);
        Task EliminarUsuario(int id);
        Task<int> ContarUsuariosClientes();
    }
}
