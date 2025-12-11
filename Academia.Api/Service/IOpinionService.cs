using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Services
{
    public interface IOpinionService
    {
        Task<List<OpinionDTO>> GetAllAsync();
        Task<List<OpinionDTO>> GetAllAsyncParams(OpinionParameters parameters);
        Task<OpinionStatsDTO> GetStats();
        Task<OpinionDTO?> GetByIdAsync(int id);
        Task<OpinionDTO> AddAsync(OpinionCreateDTO opinionCreateDTO);
        Task UpdateAsync(OpinionDTO opinionDTO);
        Task DeleteAsync(int id);
    }
}