using Academia.Api.Models;
using Academia.Api.Models.DTO;
using Academia.Api.Repositories;

namespace Academia.Api.Services
{
    public class InscripcionService : IInscripcionService
    {
        private readonly IInscripcionRepository _inscripcionRepository;

        public InscripcionService(IInscripcionRepository inscripcionRepository)
        {
            _inscripcionRepository = inscripcionRepository;
        }

        public async Task<List<InscripcionDTO>> GetAllAsync()
        {
            var inscripciones = await _inscripcionRepository.GetAllAsync();

            return inscripciones.Select(i => new InscripcionDTO
            {
                Id = i.Id,
                Progreso = i.Progreso,
                Comentario = i.Comentario,
                Nota = i.Nota,
                Activa = i.Activa,
                InscripcionFecha = i.InscripcionFecha,
                UsuarioId = i.UsuarioId,
                CursoId = i.CursoId
            }).ToList();
        }

        public async Task<InscripcionDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var inscripcion = await _inscripcionRepository.GetByIdAsync(id);

            if (inscripcion == null)
                return null;

            return new InscripcionDTO
            {
                Id = inscripcion.Id,
                Progreso = inscripcion.Progreso,
                Comentario = inscripcion.Comentario,
                Nota = inscripcion.Nota,
                Activa = inscripcion.Activa,
                InscripcionFecha = inscripcion.InscripcionFecha,
                UsuarioId = inscripcion.UsuarioId,
                CursoId = inscripcion.CursoId
            };
        }

        public async Task <InscripcionDTO> AddAsync(InscripcionCreateDTO inscripcionCreateDTO)
        {
            var inscripcion = new Inscripcion
            {
                Progreso = inscripcionCreateDTO.Progreso,
                Comentario = inscripcionCreateDTO.Comentario,
                Nota = inscripcionCreateDTO.Nota,
                Activa = inscripcionCreateDTO.Activa,
                InscripcionFecha = inscripcionCreateDTO.InscripcionFecha,
                UsuarioId = inscripcionCreateDTO.UsuarioId,
                CursoId = inscripcionCreateDTO.CursoId    
            };

            await _inscripcionRepository.AddAsync(inscripcion);

            return new InscripcionDTO
            {
                Id = inscripcion.Id,
                Progreso = inscripcion.Progreso,
                Comentario = inscripcion.Comentario,
                Nota = inscripcion.Nota,
                Activa = inscripcion.Activa,
                InscripcionFecha = inscripcion.InscripcionFecha,
                UsuarioId = inscripcion.UsuarioId,
                CursoId = inscripcion.CursoId
            };
        }

        public async Task UpdateAsync(InscripcionDTO inscripcionDTO)
        {
            if (inscripcionDTO.Id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var inscripcion = new Inscripcion
            {
                Id = inscripcionDTO.Id,
                Progreso = inscripcionDTO.Progreso,
                Comentario = inscripcionDTO.Comentario,
                Nota = inscripcionDTO.Nota,
                Activa = inscripcionDTO.Activa,
                InscripcionFecha = inscripcionDTO.InscripcionFecha,
                UsuarioId = inscripcionDTO.UsuarioId,
                CursoId = inscripcionDTO.CursoId
            };

            await _inscripcionRepository.UpdateAsync(inscripcion);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            await _inscripcionRepository.DeleteAsync(id);
        }
    }
}