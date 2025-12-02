using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;

namespace Academia.Api.Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> GetAllAsync(UsuarioParameters parameters);
        Task<UsuarioDTO?> GetByIdAsync(int id);
        Task<UsuarioDTO> AddAsync(UsuarioCreateDTO usuarioCreateDto);
        Task UpdateAsync(UsuarioDTO usuarioDto);
        Task DeleteAsync(int id);
    }
}