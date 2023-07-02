using Gestion_Usuarios.Model;

namespace Gestion_Usuarios.Services
{
    public interface IUsuariosService
    {
        public interface IUsuarioService
        {
            Task<Usuario> CrearUsuario(Usuario usuario);

            Task<bool> EliminarUsuario(int id);

            Task<bool> ModificarUsuario(int id, Usuario usuario);
        }
    }
}