using System.ComponentModel.DataAnnotations;
namespace Academia.Api.Models.DTO
{
    public class UsuarioCreateDTO
    {

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El email es obligatorio.")]
        public string Email { get; set; } = string.Empty;
        public decimal Creditos { get; set; }
        public int Cursos { get; set; }
        public bool Premium { get; set; }
        public DateTime Registro { get; set; }
    }
}