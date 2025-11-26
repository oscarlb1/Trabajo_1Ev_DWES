namespace Academia.Api.Models
{
    public class Curso
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;
        public string Detalle { get; set; } = string.Empty;

        public decimal Costo { get; set; }
        public int Horas { get; set; }
        public bool Publicado { get; set; }
        public DateTime Creacion { get; set; }
        public int ProfesorId { get; set; }
        public int MateriaId { get; set; }
        
        public Curso() { }
    }
}