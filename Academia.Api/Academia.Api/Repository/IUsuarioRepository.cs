using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllAsync(UsuarioParameters parameters);
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
    }
}