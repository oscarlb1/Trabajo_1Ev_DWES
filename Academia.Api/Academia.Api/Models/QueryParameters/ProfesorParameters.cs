namespace Academia.Api.Models.QueryParameters
{
    public class ProfesorParameters
    {
        public string? Especialidad { get; set; } = string.Empty; 
        public string? OrderBy { get; set; } = string.Empty; 
        public string SortDirection { get; set; } = "asc"; 
    }
}