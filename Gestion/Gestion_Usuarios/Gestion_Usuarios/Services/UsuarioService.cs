using Gestion_Usuarios.Context;
using Gestion_Usuarios.Model;
using static Gestion_Usuarios.Services.IUsuariosService;

namespace Gestion_Usuarios.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _dbContext;

        public UsuarioService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre)) { throw new ArgumentException("El nombre no puede estar vacío"); }

            if (!IsValidEmail(usuario.Email)) { throw new ArgumentException("El correo electrónico no es válido"); }

            usuario.FechaCreacion = DateTime.Today;

            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) { throw new ArgumentException("El correo electrónico no puede estar vacío"); }

            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            var usuario = await _dbContext.Usuarios.FindAsync(id);

            if (usuario == null) { throw new ArgumentException("El usuario que desea eliminar no existe"); }

            _dbContext.Usuarios.Remove(usuario);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ModificarUsuario(int id, Usuario usuario)
        {
            var usuarioExistente = await _dbContext.Usuarios.FindAsync(id);

            if (usuarioExistente == null) { throw new ArgumentException("El usuario que desea modificar no existe"); }

            if (string.IsNullOrWhiteSpace(usuarioExistente.Nombre)) { throw new ArgumentException("El nombre no puede estar vacío"); }

            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Email = usuario.Email;

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}