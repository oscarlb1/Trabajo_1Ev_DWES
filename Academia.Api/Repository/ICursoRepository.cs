using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Repositories
{
    public interface ICursoRepository
    {
        Task<List<Curso>> GetAllAsync(CursoParameters parameters);
        Task<Curso?> GetByIdAsync(int id);
        Task <Curso>AddAsync(Curso curso);
        Task UpdateAsync(Curso curso);
        Task DeleteAsync(int id);
    }
}