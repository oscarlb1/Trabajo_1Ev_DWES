using Academia.Api.Models.DTO;

namespace Academia.Api.Services
{
    public interface IProfesorService
        {
        Task<List<ProfesorDTO>> GetAllAsync();
        Task<ProfesorDTO?> GetByIdAsync(int id);
        Task<ProfesorDTO> AddAsync(ProfesorCreateDTO profesorCreateDto);
        Task UpdateAsync(ProfesorDTO profesorDTO);
        Task DeleteAsync(int id);
        }
}