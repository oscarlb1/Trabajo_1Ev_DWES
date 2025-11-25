public class Profesor {
    public int Id { get; set; }

        public string Nombre { get; set; } = "";
        public string Especialidad { get; set; } = "";
        public decimal Salario { get; set; }
        public int Experiencia { get; set; }
        public bool Certificado { get; set; }
        public DateTime Contrato { get; set; }
        public string Email { get; set; } = "";

        public Profesor() { }
}