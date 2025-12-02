using Academia.Api.Models.DTO;

namespace Academia.Api.Services
{
    public interface IInscripcionService
        {
        Task<List<InscripcionDTO>> GetAllAsync();
        Task<InscripcionDTO?> GetByIdAsync(int id);
        Task<InscripcionDTO> AddAsync(InscripcionCreateDTO inscripcionCreateDTO);
        Task UpdateAsync(InscripcionDTO inscripcionDTO);
        Task DeleteAsync(int id);
        }
}