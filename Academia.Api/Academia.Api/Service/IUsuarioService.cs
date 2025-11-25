using Academia.Api.Models.DTO;

namespace Academia.Api.Services
{
    public interface IUsuarioService
        {
        Task<List<UsuarioDTO>> GetAllAsync();
        Task<UsuarioDTO?> GetByIdAsync(int id);
        Task<UsuarioDTO> AddAsync(UsuarioCreateDTO usuarioCreateDto);
        Task UpdateAsync(UsuarioDTO usuarioDto);
        Task DeleteAsync(int id);
        }
}