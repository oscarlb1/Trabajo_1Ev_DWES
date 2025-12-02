namespace Academia.Api.Models.QueryParameters
{
    public class MateriaParameters
    {
        public string? Detalle { get; set; } = string.Empty; 
        public string? OrderBy { get; set; } = string.Empty; 
        public string SortDirection { get; set; } = "asc"; 
    }
}