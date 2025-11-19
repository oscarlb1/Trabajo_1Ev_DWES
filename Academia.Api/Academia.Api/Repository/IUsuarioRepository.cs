namespace Academia.Api.Repositories
{
    public interface IUsuarioRepository
        {
            Task<List<Usuario>> GetAllAsync();
            Task<Usuario?> GetByIdAsync(int id);
        }
}