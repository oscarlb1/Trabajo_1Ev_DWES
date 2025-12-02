namespace Academia.Api.Repositories
{
    public interface ILeccionRepository
        {
            Task<List<Leccion>> GetAllAsync();
            Task<Leccion?> GetByIdAsync(int id);
            Task<Leccion> AddAsync(Leccion leccion);
            Task UpdateAsync(Leccion leccion);
            Task DeleteAsync(int id);
        }
}