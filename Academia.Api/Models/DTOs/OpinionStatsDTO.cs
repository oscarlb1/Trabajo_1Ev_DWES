namespace Academia.Api.Models.DTO
{
    public class OpinionStatsDTO
    {
        public int TotalOpiniones { get; set; }
        public decimal PuntuacionMedia { get; set; } 
        public int PuntuacionMinima { get; set; }
        public int PuntuacionMaxima { get; set; }
    }
}