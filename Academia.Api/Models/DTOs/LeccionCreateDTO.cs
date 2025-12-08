using System.ComponentModel.DataAnnotations;

namespace Academia.Api.Models.DTO
{   
    public class LeccionCreateDTO
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Titulo { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        [Range(1, 180, ErrorMessage = "Los minutos deben estar entre 1 y 180.")]
        public int Minutos { get; set; }
        public bool Examen { get; set; }
        public DateTime Publicacion { get; set; }
        public string URL { get; set; } = string.Empty;
        public int CursoId { get; set; }
    }
}