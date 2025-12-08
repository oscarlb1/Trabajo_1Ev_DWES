namespace Academia.Api.Models.DTO
{
    public class ProfesorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public decimal Salario { get; set; }
        public int Experiencia { get; set; }
        public bool Certificado { get; set; }
        public DateTime Contrato { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
