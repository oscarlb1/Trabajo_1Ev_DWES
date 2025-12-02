using System.ComponentModel.DataAnnotations;
namespace Academia.Api.Models.DTO
{
    public class InscripcionCreateDTO
    {
        [Required(ErrorMessage = "El progreso es obligatorio.")]
        public string Progreso { get; set; } = string.Empty;
        public string? Comentario { get; set; }
        [Range(0.0, 10.0, ErrorMessage = "La nota debe estar entre 0.0 y 10.0.")]
        public decimal Nota { get; set; }
        public bool Activa { get; set; }
        public DateTime InscripcionFecha { get; set; }
        public int UsuarioId { get; set; }
        public int CursoId { get; set; }
    }
}