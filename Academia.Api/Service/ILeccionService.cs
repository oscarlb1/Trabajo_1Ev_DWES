using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Services
{
    public interface ILeccionService
    {
        Task<List<LeccionDTO>> GetAllAsync(LeccionParameters parameters);
        Task<LeccionDTO?> GetByIdAsync(int id);
        Task<LeccionDTO> AddAsync(LeccionCreateDTO leccionCreateDTO);
        Task UpdateAsync(LeccionDTO leccionDTO);
        Task DeleteAsync(int id);
    }
}