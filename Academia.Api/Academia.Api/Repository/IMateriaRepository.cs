using Academia.Api.Models;
using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Repositories
{
    public interface IMateriaRepository
    {
        Task<List<Materia>> GetAllAsync(MateriaParameters parameters);
        Task<Materia?> GetByIdAsync(int id);
        Task <Materia>AddAsync(Materia materia);
        Task UpdateAsync(Materia materia);
        Task DeleteAsync(int id);
    }
}