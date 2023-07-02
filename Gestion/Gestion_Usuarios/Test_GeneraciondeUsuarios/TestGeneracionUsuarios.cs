using Gestion_Usuarios.Context;
using Gestion_Usuarios.Model;
using Gestion_Usuarios.Services;
using Microsoft.EntityFrameworkCore;

namespace Test_GeneraciondeUsuarios
{
    [TestFixture]
    public class Tests
    {
        private UsuarioService _usuarioService;
        private AppDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            // Configuración de la prueba
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new AppDbContext(options);
            _usuarioService = new UsuarioService(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            // Limpieza después de cada prueba
            _dbContext.Dispose();
        }

        [Test]
        public async Task CrearUsuario_WithValidData_ReturnsNuevoUsuario()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Jose Bertoni", Email = "jose.bertoni@gmail.com" };

            // Act
            var nuevoUsuario = await _usuarioService.CrearUsuario(usuario);

            // Assert
            Assert.IsNotNull(nuevoUsuario);
            Assert.AreEqual(usuario.Nombre, nuevoUsuario.Nombre);
            Assert.AreEqual(usuario.Email, nuevoUsuario.Email);
            Assert.AreEqual(DateTime.Today, nuevoUsuario.FechaCreacion);
        }

        [Test]
        public async Task CrearUsuario_WithInvalidNombre_ThrowsArgumentException()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "", Email = "jose.bertoni@gmail.com" };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _usuarioService.CrearUsuario(usuario);
            });
        }

        [Test]
        public async Task CrearUsuario_WithInvalidEmail_ThrowsArgumentException()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Jose Bertoni", Email = "jose.bertoni" };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _usuarioService.CrearUsuario(usuario);
            });
        }

        [Test]
        public async Task EliminarUsuario_WithExistingId_ReturnsTrue()
        {
            // Arrange
            var usuario = new Usuario { Nombre = "Jose Bertoni", Email = "jose.bertoni@gmail.com" };
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _usuarioService.EliminarUsuario(usuario.ID);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task EliminarUsuario_WithNonExistingId_ThrowsArgumentException()
        {
            // Arrange
            var nonExistingId = 999;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _usuarioService.EliminarUsuario(nonExistingId);
            });
        }

        [Test]
        public async Task ModificarUsuario_WithExistingId_ReturnsTrue()
        {
            // Arrange
            var usuarioExistente = new Usuario { Nombre = "Jose Bertoni", Email = "jose.bertoni@gmail.com" };
            _dbContext.Usuarios.Add(usuarioExistente);
            await _dbContext.SaveChangesAsync();

            var usuarioModificado = new Usuario { Nombre = "Nicolas Bertoni", Email = "nicolas.bertoni@gmail.com" };

            // Act
            var result = await _usuarioService.ModificarUsuario(usuarioExistente.ID, usuarioModificado);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(usuarioModificado.Nombre, usuarioExistente.Nombre);
            Assert.AreEqual(usuarioModificado.Email, usuarioExistente.Email);
        }

        [Test]
        public async Task ModificarUsuario_WithNonExistingId_ThrowsArgumentException()
        {
            // Arrange
            var nonExistingId = 999;
            var usuarioModificado = new Usuario { Nombre = "Nicolas Bertoni", Email = "nicolas.bertoni@gmail.com" };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _usuarioService.ModificarUsuario(nonExistingId, usuarioModificado);
            });
        }
    }
}