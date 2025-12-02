namespace Academia.Api.Models.QueryParameters
{
    public class UsuarioParameters
    {
        public string? Email { get; set; } = string.Empty; 
        public string? OrderBy { get; set; } = string.Empty; 
        public string SortDirection { get; set; } = "asc"; 
    }
}