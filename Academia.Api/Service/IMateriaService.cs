using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Services
{
    public interface IMateriaService
    {
        Task<List<MateriaDTO>> GetAllAsync(MateriaParameters parameters);
        Task<MateriaDTO?> GetByIdAsync(int id);
        Task<MateriaDTO> AddAsync(MateriaCreateDTO materiaCreateDTO);
        Task UpdateAsync(MateriaDTO materiaDTO);
        Task DeleteAsync(int id);
    }
}