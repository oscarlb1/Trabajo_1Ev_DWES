using Academia.Api.Models;
using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Repositories
{
    public interface ILeccionRepository
    {
        Task<List<Leccion>> GetAllAsync(LeccionParameters parameters);
        Task<Leccion?> GetByIdAsync(int id);
        Task AddAsync(Leccion leccion);
        Task UpdateAsync(Leccion leccion);
        Task DeleteAsync(int id);
    }
}