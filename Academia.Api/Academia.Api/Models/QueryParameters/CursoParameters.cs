namespace Academia.Api.Models.QueryParameters
{
    public class CursoParameters
    {
        public string? Titulo { get; set; } = string.Empty; 
        public string? OrderBy { get; set; } = string.Empty; 
        public string SortDirection { get; set; } = "asc"; 
    }
}