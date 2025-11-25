namespace Academia.Api.Repositories
{
    public interface IUsuarioRepository
        {
            Task<List<Usuario>> GetAllAsync();
            Task<Usuario?> GetByIdAsync(int id);
            Task<Usuario> AddAsync(Usuario usuario);
            Task UpdateAsync(Usuario usuario);
            Task DeleteAsync(int id);
        }
}