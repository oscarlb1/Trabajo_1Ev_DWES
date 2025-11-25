namespace Academia.Api.Repositories
{
    public interface IProfesorRepository
        {
            Task<List<Profesor>> GetAllAsync();
            Task<Profesor?> GetByIdAsync(int id);
            Task<Profesor> AddAsync(Profesor profesor);
            Task UpdateAsync(Profesor profesor);
            Task DeleteAsync(int id);
        }
}