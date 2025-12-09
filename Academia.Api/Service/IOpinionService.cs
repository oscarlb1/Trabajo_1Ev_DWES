using Academia.Api.Models.DTO;

namespace Academia.Api.Services
{
    public interface IOpinionService
    {
        Task<List<OpinionDTO>> GetAllAsync();
        Task<OpinionDTO?> GetByIdAsync(int id);
        Task<OpinionDTO> AddAsync(OpinionCreateDTO opinionCreateDTO);
        Task UpdateAsync(OpinionDTO opinionDTO);
        Task DeleteAsync(int id);
    }
}