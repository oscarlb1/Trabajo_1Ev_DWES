using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Services
{
    public interface IProfesorService
    {
        Task<List<ProfesorDTO>> GetAllAsync(ProfesorParameters parameters);
        Task<ProfesorDTO?> GetByIdAsync(int id);
        Task<ProfesorDTO> AddAsync(ProfesorCreateDTO profesorCreateDto);
        Task UpdateAsync(ProfesorDTO profesorDTO);
        Task DeleteAsync(int id);
    }
}