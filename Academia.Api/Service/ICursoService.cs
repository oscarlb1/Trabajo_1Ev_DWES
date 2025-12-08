using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Services
{
    public interface ICursoService
    {
        Task<List<CursoDTO>> GetAllAsync(CursoParameters parameters);
        Task<CursoDTO?> GetByIdAsync(int id);
        Task<CursoDTO> AddAsync(CursoCreateDTO cursoCreateDto);
        Task UpdateAsync(CursoDTO cursoDTO);
        Task DeleteAsync(int id);
    }
}