using System.ComponentModel.DataAnnotations;
namespace Academia.Api.Models.DTO
{
    public class CursoCreateDTO
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Titulo { get; set; } = string.Empty;
        public string Detalle { get; set; } = string.Empty;

        public decimal Costo { get; set; }
        public int Horas { get; set; }
        public bool Publicado { get; set; }
        public DateTime Creacion { get; set; }
        [Required(ErrorMessage = "El ID del profesor es obligatorio.")]
        public int ProfesorId { get; set; }
        [Required(ErrorMessage = "El ID de la materia es obligatorio.")]
        public int MateriaId { get; set; }
    }
}