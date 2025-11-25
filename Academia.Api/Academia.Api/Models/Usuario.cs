public class Usuario {

        public int Id { get; set; }

        public string Nombre { get; set; } = "";  
        public string Email { get; set; } = "";

        public decimal Creditos { get; set; }

        public int Cursos { get; set; }

        public bool Premium { get; set; }

        public DateTime Registro { get; set; }

public Usuario(){}

}