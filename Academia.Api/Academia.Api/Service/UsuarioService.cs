using Academia.Api.Models.DTO;
using Academia.Api.Repositories;
using Academia.Api.Models; // Asumo que el modelo Usuario está aquí o en Models
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Academia.Api.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuarioDTO>> GetAllAsync()
        {

            var usuarios = await _usuarioRepository.GetAllAsync();

            var usuariosDto = usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email,
                Creditos = u.Creditos,
                Cursos = u.Cursos,
                Premium = u.Premium,
                Registro = u.Registro
            }).ToList();

            return usuariosDto;
        }

        public async Task<UsuarioDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
            {
                return null;
            }
            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
            };
        }
    }
}