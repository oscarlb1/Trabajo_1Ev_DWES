namespace Academia.Api.Models.DTO
{
    public class OpinionDTO 
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaComentario { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public int Puntuacion { get; set; }
        public int CursoId { get; set; }
    }
}
