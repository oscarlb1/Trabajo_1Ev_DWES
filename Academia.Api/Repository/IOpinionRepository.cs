using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Repositories
{
    public interface IOpinionRepository
    {
        Task<List<Opinion>> GetAllAsync();
        Task<List<Opinion>> GetAllAsyncParams(OpinionParameters parameters);
        Task<List<Opinion>> GetStats();
        Task<Opinion?> GetByIdAsync(int id);
        Task <Opinion>AddAsync(Opinion opinion);
        Task UpdateAsync(Opinion opinion);
        Task DeleteAsync(int id);
    }
}