using Academia.Api.Models;
using Academia.Api.Models.DTO;
using Academia.Api.Repositories;

namespace Academia.Api.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuarioDTO>> GetAllAsync(Models.QueryParameters.UsuarioParameters parameters)
        {
            // Validaciones de parámetros
            if (!string.IsNullOrEmpty(parameters.OrderBy) && 
                parameters.OrderBy != "Registro" && 
                parameters.OrderBy != "Creditos")
            {
                throw new ArgumentException("El campo de ordenamiento no es válido. Use 'Registro' o 'Creditos'.");
            }

            var usuarios = await _usuarioRepository.GetAllAsync(parameters);

            return usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email,
                Creditos = u.Creditos,
                Cursos = u.Cursos,
                Premium = u.Premium,
                Registro = u.Registro
            }).ToList();
        }

        public async Task<UsuarioDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
                return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Creditos = usuario.Creditos,
                Cursos = usuario.Cursos,
                Premium = usuario.Premium,
                Registro = usuario.Registro
            };
        }

        public async Task <UsuarioDTO> AddAsync(UsuarioCreateDTO usuarioCreateDto)
        {
            var usuario = new Usuario
            {
                Nombre = usuarioCreateDto.Nombre,
                Email = usuarioCreateDto.Email,
                Creditos = usuarioCreateDto.Creditos,
                Cursos = usuarioCreateDto.Cursos,
                Premium = usuarioCreateDto.Premium,
                Registro = usuarioCreateDto.Registro
            };

            await _usuarioRepository.AddAsync(usuario);

            return new UsuarioDTO
            {
                Id = usuario.Id, 
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Creditos = usuario.Creditos,
                Cursos = usuario.Cursos,
                Premium = usuario.Premium,
                Registro = usuario.Registro
            };
        }

        public async Task UpdateAsync(UsuarioDTO usuarioDto)
        {
            if (usuarioDto.Id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var usuario = new Usuario
            {
                Id = usuarioDto.Id,
                Nombre = usuarioDto.Nombre,
                Email = usuarioDto.Email,
                Creditos = usuarioDto.Creditos,
                Cursos = usuarioDto.Cursos,
                Premium = usuarioDto.Premium,
                Registro = usuarioDto.Registro
            };

            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            await _usuarioRepository.DeleteAsync(id);
        }
    }
}