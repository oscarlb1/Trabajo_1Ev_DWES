using Academia.Api.Models.DTO;

namespace Academia.Api.Services
{
    public interface ICursoService
        {
        Task<List<CursoDTO>> GetAllAsync();
        Task<CursoDTO?> GetByIdAsync(int id);
        Task<CursoDTO> AddAsync(CursoCreateDTO cursoCreateDto);
        Task UpdateAsync(CursoDTO cursoDTO);
        Task DeleteAsync(int id);
        }
}