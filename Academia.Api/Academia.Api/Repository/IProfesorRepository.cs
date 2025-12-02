using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Repositories
{
    public interface IProfesorRepository
    {
        Task<List<Profesor>> GetAllAsync(ProfesorParameters parameters);
        Task<Profesor?> GetByIdAsync(int id);
        Task AddAsync(Profesor profesor);
        Task UpdateAsync(Profesor profesor);
        Task DeleteAsync(int id);
    }
}