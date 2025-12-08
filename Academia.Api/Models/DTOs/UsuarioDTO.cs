namespace Academia.Api.Models.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Creditos { get; set; }
        public int Cursos { get; set; }
        public bool Premium { get; set; }
        public DateTime Registro { get; set; }
    }
}