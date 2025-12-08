using Academia.Api.Models;
using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;
using Academia.Api.Repositories;

namespace Academia.Api.Services
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IProfesorRepository _profesorRepository;
        private readonly IMateriaRepository _materiaRepository;

        public CursoService(
            ICursoRepository cursoRepository,
            IProfesorRepository profesorRepository, 
            IMateriaRepository materiaRepository)   
        {
            _cursoRepository = cursoRepository;
            _profesorRepository = profesorRepository; 
            _materiaRepository = materiaRepository;   
        }

        public async Task<List<CursoDTO>> GetAllAsync(CursoParameters parameters)
        {
            // Validaciones de parámetros
            if (!string.IsNullOrEmpty(parameters.OrderBy) && 
                parameters.OrderBy != "Horas" && 
                parameters.OrderBy != "Costo")
            {
                throw new ArgumentException("El campo de ordenamiento no es válido. Use 'Horas' o 'Costo'.");
            }

            var cursos = await _cursoRepository.GetAllAsync(parameters);

            return cursos.Select(c => new CursoDTO
            {
                Id = c.Id,
                Titulo = c.Titulo,
                Detalle = c.Detalle,
                Costo = c.Costo,
                Horas = c.Horas,
                Publicado = c.Publicado,
                Creacion = c.Creacion,
                ProfesorId = c.ProfesorId,
                MateriaId = c.MateriaId
            }).ToList();
        }

        public async Task<CursoDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var curso = await _cursoRepository.GetByIdAsync(id);

            if (curso == null)
                return null;

            return new CursoDTO
            {
                Id = curso.Id,
                Titulo = curso.Titulo,
                Detalle = curso.Detalle,
                Costo = curso.Costo,
                Horas = curso.Horas,
                Publicado = curso.Publicado,
                Creacion = curso.Creacion,
                ProfesorId = curso.ProfesorId,
                MateriaId = curso.MateriaId
            };
        }

        public async Task <CursoDTO> AddAsync(CursoCreateDTO cursoCreateDTO)
        {
            // Verificar si el ProfesorId existe
            var profesorExiste = await _profesorRepository.GetByIdAsync(cursoCreateDTO.ProfesorId);
            if (profesorExiste == null)
            {
                throw new KeyNotFoundException($"El Profesor con ID {cursoCreateDTO.ProfesorId} no existe."); 
            }

            // Verificar si la MateriaId existe
            var materiaExiste = await _materiaRepository.GetByIdAsync(cursoCreateDTO.MateriaId);
            if (materiaExiste == null)
            {
                throw new KeyNotFoundException($"La Materia con ID {cursoCreateDTO.MateriaId} no existe.");
            }

            var curso = new Curso
            {
                Titulo = cursoCreateDTO.Titulo,
                Detalle = cursoCreateDTO.Detalle,
                Costo = cursoCreateDTO.Costo,
                Horas = cursoCreateDTO.Horas,
                Publicado = cursoCreateDTO.Publicado,
                Creacion = cursoCreateDTO.Creacion,
                ProfesorId = cursoCreateDTO.ProfesorId,
                MateriaId = cursoCreateDTO.MateriaId
            };

            await _cursoRepository.AddAsync(curso);

            return new CursoDTO
            {
                Id = curso.Id, 
                Titulo = curso.Titulo,
                Detalle = curso.Detalle,
                Costo = curso.Costo,
                Horas = curso.Horas,
                Publicado = curso.Publicado,
                Creacion = curso.Creacion,
                ProfesorId = curso.ProfesorId,
                MateriaId = curso.MateriaId
            };
        }

        public async Task UpdateAsync(CursoDTO cursoDTO)
        {
            if (cursoDTO.Id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var cursoExistente = await _cursoRepository.GetByIdAsync(cursoDTO.Id);
            if (cursoExistente == null)
            {
                // Verficiar que el curso existe antes de actualizar
                throw new KeyNotFoundException($"El Curso con ID {cursoDTO.Id} no existe y no puede ser actualizado.");
            }

            // Verificar si el ProfesorId existe
            var profesorExiste = await _profesorRepository.GetByIdAsync(cursoDTO.ProfesorId);
            if (profesorExiste == null)
            {
                throw new KeyNotFoundException($"El Profesor con ID {cursoDTO.ProfesorId} no existe.");
            }

            // 3. Verificar si la MateriaId existe
            var materiaExiste = await _materiaRepository.GetByIdAsync(cursoDTO.MateriaId);
            if (materiaExiste == null)
            {
                throw new KeyNotFoundException($"La Materia con ID {cursoDTO.MateriaId} no existe.");
            }

            var curso = new Curso
            {
                Id = cursoDTO.Id,
                Titulo = cursoDTO.Titulo,
                Detalle = cursoDTO.Detalle,
                Costo = cursoDTO.Costo,
                Horas = cursoDTO.Horas,
                Publicado = cursoDTO.Publicado,
                Creacion = cursoDTO.Creacion,
                ProfesorId = cursoDTO.ProfesorId,
                MateriaId = cursoDTO.MateriaId
            };

            await _cursoRepository.UpdateAsync(curso);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            await _cursoRepository.DeleteAsync(id);
        }
    }
}