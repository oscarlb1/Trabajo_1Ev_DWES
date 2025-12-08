using System.ComponentModel.DataAnnotations;
namespace Academia.Api.Models.DTO
{
    public class ProfesorCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public decimal Salario { get; set; }
        [Range(0, 50, ErrorMessage = "La experiencia debe estar entre 0 y 50 años.")]
        public int Experiencia { get; set; }
        public bool Certificado { get; set; }
        public DateTime Contrato { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
