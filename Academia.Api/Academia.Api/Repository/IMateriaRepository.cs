namespace Academia.Api.Repositories
{
    public interface IMateriaRepository
        {
            Task<List<Materia>> GetAllAsync();
            Task<Materia?> GetByIdAsync(int id);
            Task<Materia> AddAsync(Materia materia);
            Task UpdateAsync(Materia materia);
            Task DeleteAsync(int id);
        }
}