using Academia.Api.Models.DTO;

namespace Academia.Api.Services
{
    public interface ILeccionService
        {
        Task<List<LeccionDTO>> GetAllAsync();
        Task<LeccionDTO?> GetByIdAsync(int id);
        Task<LeccionDTO> AddAsync(LeccionCreateDTO leccionCreateDTO);
        Task UpdateAsync(LeccionDTO leccionDTO);
        Task DeleteAsync(int id);
        }
}