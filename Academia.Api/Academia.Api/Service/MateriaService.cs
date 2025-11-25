using Academia.Api.Models;
using Academia.Api.Models.DTO;
using Academia.Api.Repositories;

namespace Academia.Api.Services
{
    public class MateriaService : IMateriaService
    {
        private readonly IMateriaRepository _materiaRepository;

        public MateriaService(IMateriaRepository materiaRepository)
        {
            _materiaRepository = materiaRepository;
        }

        public async Task<List<MateriaDTO>> GetAllAsync()
        {
            var materias = await _materiaRepository.GetAllAsync();

            return materias.Select(m => new MateriaDTO
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Detalle = m.Detalle,
                Nivel = m.Nivel,
                Cantidad = m.Cantidad,
                Obligatoria = m.Obligatoria,
                Creacion = m.Creacion
            }).ToList();
        }

        public async Task<MateriaDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var materia = await _materiaRepository.GetByIdAsync(id);

            if (materia == null)
                return null;

            return new MateriaDTO
            {
                Id = materia.Id,
                Nombre = materia.Nombre,
                Detalle = materia.Detalle,
                Nivel = materia.Nivel,
                Cantidad = materia.Cantidad,
                Obligatoria = materia.Obligatoria,
                Creacion = materia.Creacion
            };
        }

        public async Task <MateriaDTO> AddAsync(MateriaCreateDTO materiaCreateDTO)
        {
            var materia = new Materia
            {
                Nombre = materiaCreateDTO.Nombre,
                Detalle = materiaCreateDTO.Detalle,
                Nivel = materiaCreateDTO.Nivel,
                Cantidad = materiaCreateDTO.Cantidad,
                Obligatoria = materiaCreateDTO.Obligatoria,
                Creacion = materiaCreateDTO.Creacion    
            };

            await _materiaRepository.AddAsync(materia);

            return new MateriaDTO
            {
                Id = materia.Id, 
                Nombre = materia.Nombre,
                Detalle = materia.Detalle,
                Nivel = materia.Nivel,
                Cantidad = materia.Cantidad,
                Obligatoria = materia.Obligatoria,
                Creacion = materia.Creacion
            };
        }

        public async Task UpdateAsync(MateriaDTO materiaDTO)
        {
            if (materiaDTO.Id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var materia = new Materia
            {
                Id = materiaDTO.Id, 
                Nombre = materiaDTO.Nombre,
                Detalle = materiaDTO.Detalle,
                Nivel = materiaDTO.Nivel,
                Cantidad = materiaDTO.Cantidad,
                Obligatoria = materiaDTO.Obligatoria,
                Creacion = materiaDTO.Creacion
            };

            await _materiaRepository.UpdateAsync(materia);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            await _materiaRepository.DeleteAsync(id);
        }
    }
}