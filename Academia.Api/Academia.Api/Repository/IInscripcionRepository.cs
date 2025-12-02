namespace Academia.Api.Repositories
{
    public interface IInscripcionRepository
        {
            Task<List<Inscripcion>> GetAllAsync();
            Task<Inscripcion?> GetByIdAsync(int id);
            Task<Inscripcion> AddAsync(Inscripcion inscripcion);
            Task UpdateAsync(Inscripcion inscripcion);
            Task DeleteAsync(int id);
        }
}