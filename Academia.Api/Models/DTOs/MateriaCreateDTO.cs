using System.ComponentModel.DataAnnotations;
namespace Academia.Api.Models.DTO
{   
    public class MateriaCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }  = string.Empty;
        public string Detalle { get; set; }  = string.Empty;
        [Range(1.0, 10.0, ErrorMessage = "El nivel debe estar entre 1 y 10.")]
        public decimal Nivel { get; set; }
        public int Cantidad { get; set; }
        public bool Obligatoria { get; set; }
        public DateTime Creacion { get; set; }
    }
}