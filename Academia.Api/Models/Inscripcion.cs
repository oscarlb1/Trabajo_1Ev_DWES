public class Inscripcion
    {
        public int Id { get; set; }
        public string Progreso { get; set; } = string.Empty;
        public string? Comentario { get; set; }
        public decimal Nota { get; set; }
        public bool Activa { get; set; }
        public DateTime InscripcionFecha { get; set; }
        public int UsuarioId { get; set; }
        public int CursoId { get; set; }
    }
