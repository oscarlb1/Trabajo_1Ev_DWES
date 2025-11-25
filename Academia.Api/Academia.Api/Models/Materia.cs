
public class Materia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }  = "";
        public string Detalle { get; set; }  = "";
        public decimal Nivel { get; set; }
        public int Cantidad { get; set; }
        public bool Obligatoria { get; set; }
        public DateTime Creacion { get; set; }

        public Materia() { }
    }