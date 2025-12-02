namespace Academia.Api.Models.DTO
{   
    public class LeccionCreateDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int Minutos { get; set; }
        public bool Examen { get; set; }
        public DateTime Publicacion { get; set; }
        public string URL { get; set; } = string.Empty;
        public int CursoId { get; set; }
    }
}