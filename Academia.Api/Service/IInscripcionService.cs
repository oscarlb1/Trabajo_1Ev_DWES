using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Services
{
    public interface IInscripcionService
    {
        Task<List<InscripcionDTO>> GetAllAsync(InscripcionParameters parameters);
        Task<InscripcionDTO?> GetByIdAsync(int id);
        Task<InscripcionDTO> AddAsync(InscripcionCreateDTO inscripcionCreateDTO);
        Task UpdateAsync(InscripcionDTO inscripcionDTO);
        Task DeleteAsync(int id);
    }
}