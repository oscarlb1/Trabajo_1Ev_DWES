using Academia.Api.Models.DTO;

namespace Academia.Api.Services
{
    public interface IMateriaService
        {
        Task<List<MateriaDTO>> GetAllAsync();
        Task<MateriaDTO?> GetByIdAsync(int id);
        Task<MateriaDTO> AddAsync(MateriaCreateDTO materiaCreateDTO);
        Task UpdateAsync(MateriaDTO materiaDTO);
        Task DeleteAsync(int id);
        }
}