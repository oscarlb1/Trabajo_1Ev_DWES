using Academia.Api.Models;
using Academia.Api.Models.DTO;
using Academia.Api.Repositories;

namespace Academia.Api.Services
{
    public class LeccionService : ILeccionService
    {
        private readonly ILeccionRepository _leccionRepository;

        public LeccionService(ILeccionRepository leccionRepository)
        {
            _leccionRepository = leccionRepository;
        }

        public async Task<List<LeccionDTO>> GetAllAsync()
        {
            var lecciones = await _leccionRepository.GetAllAsync();

            return lecciones.Select(l => new LeccionDTO
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Tipo = l.Tipo,
                Minutos = l.Minutos,
                Examen = l.Examen,
                Publicacion = l.Publicacion,
                URL = l.URL,
                CursoId = l.CursoId
            }).ToList();
        }

        public async Task<LeccionDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var leccion = await _leccionRepository.GetByIdAsync(id);

            if (leccion == null)
                return null;

            return new LeccionDTO
            {
                Id = leccion.Id,
                Titulo = leccion.Titulo,
                Tipo = leccion.Tipo,
                Minutos = leccion.Minutos,
                Examen = leccion.Examen,
                Publicacion = leccion.Publicacion,
                URL = leccion.URL,
                CursoId = leccion.CursoId
            };
        }

        public async Task <LeccionDTO> AddAsync(LeccionCreateDTO leccionCreateDTO)
        {
            var leccion = new Leccion
            {
                Titulo = leccionCreateDTO.Titulo,
                Tipo = leccionCreateDTO.Tipo,
                Minutos = leccionCreateDTO.Minutos,
                Examen = leccionCreateDTO.Examen,
                Publicacion = leccionCreateDTO.Publicacion,
                URL = leccionCreateDTO.URL,
                CursoId = leccionCreateDTO.CursoId    
            };

            await _leccionRepository.AddAsync(leccion);

            return new LeccionDTO
            {
                Id = leccion.Id,
                Titulo = leccion.Titulo,
                Tipo = leccion.Tipo,
                Minutos = leccion.Minutos,
                Examen = leccion.Examen,
                Publicacion = leccion.Publicacion,
                URL = leccion.URL,
                CursoId = leccion.CursoId
            };
        }

        public async Task UpdateAsync(LeccionDTO leccionDTO)
        {
            if (leccionDTO.Id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var leccion = new Leccion
            {
                Id = leccionDTO.Id,
                Titulo = leccionDTO.Titulo,
                Tipo = leccionDTO.Tipo,
                Minutos = leccionDTO.Minutos,
                Examen = leccionDTO.Examen,
                Publicacion = leccionDTO.Publicacion,
                URL = leccionDTO.URL,
                CursoId = leccionDTO.CursoId
            };

            await _leccionRepository.UpdateAsync(leccion);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            await _leccionRepository.DeleteAsync(id);
        }
    }
}