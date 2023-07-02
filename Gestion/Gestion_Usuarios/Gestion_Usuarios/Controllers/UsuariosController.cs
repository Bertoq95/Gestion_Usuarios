using Gestion_Usuarios.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Gestion_Usuarios.Services.IUsuariosService;

namespace Gestion_Usuarios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var nuevoUsuario = await _usuarioService.CrearUsuario(usuario);
                return Ok(nuevoUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al crear el usuario: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var resultado = await _usuarioService.EliminarUsuario(id);

            if (!resultado)
                return NotFound();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarUsuario(int id, [FromBody] Usuario usuario)
        {
            var resultado = await _usuarioService.ModificarUsuario(id, usuario);

            if (!resultado)
                return NotFound();

            return Ok();
        }
    }
}