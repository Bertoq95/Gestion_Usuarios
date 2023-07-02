namespace Gestion_Usuarios.Model
{
    public class Usuario
    {
        public int ID { get; set; }
        public string? Nombre { get; set; } = String.Empty;
        public string? Email { get; set; } = String.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}