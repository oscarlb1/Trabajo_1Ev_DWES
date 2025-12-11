using Academia.Api.Models;
using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;
using Academia.Api.Repositories;

namespace Academia.Api.Services
{
    public class OpinionService : IOpinionService
    {
        private readonly IOpinionRepository _opinionRepository;
        private readonly ICursoRepository _cursoRepository;
 

        public OpinionService(
            IOpinionRepository opinionRepository,
            ICursoRepository cursoRepository)   
        {
            _opinionRepository = opinionRepository;
            _cursoRepository = cursoRepository;              
        }

        public async Task<List<OpinionDTO>> GetAllAsync()
        {
            var opiniones = await _opinionRepository.GetAllAsync();

            return opiniones.Select(o => new OpinionDTO
            {
                Id = o.Id,
                Nombre = o.Nombre,
                FechaComentario = o.FechaComentario,
                Mensaje = o.Mensaje,
                Puntuacion = o.Puntuacion,
                CursoId = o.CursoId
            }).ToList();
        }

        public async Task<OpinionStatsDTO> GetStats()
        {
            var opiniones = await _opinionRepository.GetStats();

            return opiniones;
        }


        public async Task<List<OpinionDTO>> GetAllAsyncParams(OpinionParameters parameters)
        {
            // Validaciones de parámetros
            if (!string.IsNullOrEmpty(parameters.OrderBy) && 
                parameters.OrderBy != "Fecha")
            {
                throw new ArgumentException("El campo de ordenamiento no es válido. Use 'Fecha'.");
            }

            var opiniones = await _opinionRepository.GetAllAsyncParams(parameters);

            return opiniones.Select(o => new OpinionDTO
            {
                Id = o.Id,
                Nombre = o.Nombre,
                FechaComentario = o.FechaComentario,
                Mensaje = o.Mensaje,
                Puntuacion = o.Puntuacion,
                CursoId = o.CursoId
            }).ToList();
        }

        public async Task<OpinionDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var opinion = await _opinionRepository.GetByIdAsync(id);

            if (opinion == null)
                return null;

            return new OpinionDTO
            {
                Id = opinion.Id,
                Nombre = opinion.Nombre,
                FechaComentario = opinion.FechaComentario,
                Mensaje = opinion.Mensaje,
                Puntuacion = opinion.Puntuacion,
                CursoId = opinion.CursoId
            };
        }

        public async Task <OpinionDTO> AddAsync(OpinionCreateDTO opinionCreateDTO)
        {
            
            // Verificar si el CursoId existe
            var cursoExiste = await _cursoRepository.GetByIdAsync(opinionCreateDTO.CursoId);
            if (cursoExiste == null)
            {
                throw new KeyNotFoundException($"El curso con ID {opinionCreateDTO.CursoId} no existe.");
            }

            var opinion = new Opinion
            {
                Nombre = opinionCreateDTO.Nombre,
                FechaComentario = opinionCreateDTO.FechaComentario,
                Mensaje = opinionCreateDTO.Mensaje,
                Puntuacion = opinionCreateDTO.Puntuacion,
                CursoId = opinionCreateDTO.CursoId
            };

            await _opinionRepository.AddAsync(opinion);

            return new OpinionDTO
            {
                Id = opinion.Id,
                Nombre = opinion.Nombre,
                FechaComentario = opinion.FechaComentario,
                Mensaje = opinion.Mensaje,
                Puntuacion = opinion.Puntuacion,
                CursoId = opinion.CursoId
            };
        }

        public async Task UpdateAsync(OpinionDTO opinionDTO)
        {
            if (opinionDTO.Id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var opinionExistente = await _opinionRepository.GetByIdAsync(opinionDTO.Id);
            if (opinionExistente == null)
            {
                // Verificar que la opinion existe antes de actualizar
                throw new KeyNotFoundException($"La opinión con ID {opinionDTO.Id} no existe y no puede ser actualizado.");
            }

            // Verificar que el CursoId existe
            var cursoExiste = await _cursoRepository.GetByIdAsync(opinionDTO.CursoId);
            if (cursoExiste == null)
            {
                throw new KeyNotFoundException($"El curso con ID {opinionDTO.CursoId} no existe.");
            }

            var opinion = new Opinion
            {
                Id = opinionDTO.Id,
                Nombre = opinionDTO.Nombre,
                FechaComentario = opinionDTO.FechaComentario,
                Mensaje = opinionDTO.Mensaje,
                Puntuacion = opinionDTO.Puntuacion,
                CursoId = opinionDTO.CursoId
            };

            await _opinionRepository.UpdateAsync(opinion);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            await _opinionRepository.DeleteAsync(id);
        }
    }
}