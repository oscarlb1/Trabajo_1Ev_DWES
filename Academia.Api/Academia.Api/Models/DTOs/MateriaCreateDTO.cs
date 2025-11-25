namespace Academia.Api.Models.DTO
{   
    public class MateriaCreateDTO
    {
        public string Nombre { get; set; }  = string.Empty;
        public string Detalle { get; set; }  = string.Empty;
        public decimal Nivel { get; set; }
        public int Cantidad { get; set; }
        public bool Obligatoria { get; set; }
        public DateTime Creacion { get; set; }
    }
}