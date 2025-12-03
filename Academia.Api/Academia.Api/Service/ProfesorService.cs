using Academia.Api.Models;
using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;
using Academia.Api.Repositories;

namespace Academia.Api.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly IProfesorRepository _profesorRepository;

        public ProfesorService(IProfesorRepository profesorRepository)
        {
            _profesorRepository = profesorRepository;
        }

        public async Task<List<ProfesorDTO>> GetAllAsync(ProfesorParameters parameters)
        {
            // Validaciones de parámetros
            if (!string.IsNullOrEmpty(parameters.OrderBy) && 
                parameters.OrderBy != "Salario" && 
                parameters.OrderBy != "Experiencia")
            {
                throw new ArgumentException("El campo de ordenamiento no es válido. Use 'Salario' o 'Experiencia'.");
            }

            var profesores = await _profesorRepository.GetAllAsync(parameters);

            return profesores.Select(p => new ProfesorDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Especialidad = p.Especialidad,
                Salario = p.Salario,
                Experiencia = p.Experiencia,
                Certificado = p.Certificado,
                Contrato = p.Contrato,
                Email = p.Email
            }).ToList();
        }

        public async Task<ProfesorDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var profesor = await _profesorRepository.GetByIdAsync(id);

            if (profesor == null)
                return null;

            return new ProfesorDTO
            {
                Id = profesor.Id,
                Nombre = profesor.Nombre,
                Especialidad = profesor.Especialidad,
                Salario = profesor.Salario,
                Experiencia = profesor.Experiencia,
                Certificado = profesor.Certificado,
                Contrato = profesor.Contrato,
                Email = profesor.Email
            };
        }

        public async Task <ProfesorDTO> AddAsync(ProfesorCreateDTO profesorCreateDTO)
        {
            var profesor = new Profesor
            {
                Nombre = profesorCreateDTO.Nombre,
                Especialidad = profesorCreateDTO.Especialidad,
                Salario = profesorCreateDTO.Salario,
                Experiencia = profesorCreateDTO.Experiencia,
                Certificado = profesorCreateDTO.Certificado,
                Contrato = profesorCreateDTO.Contrato,
                Email = profesorCreateDTO.Email    
            };

            await _profesorRepository.AddAsync(profesor);

            return new ProfesorDTO
            {
                Id = profesor.Id, 
                Nombre = profesor.Nombre,
                Especialidad = profesor.Especialidad,
                Salario = profesor.Salario,
                Experiencia = profesor.Experiencia,
                Certificado = profesor.Certificado,
                Contrato = profesor.Contrato,
                Email = profesor.Email
            };
        }

        public async Task UpdateAsync(ProfesorDTO profesorDTO)
        {
            if (profesorDTO.Id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var profesor = new Profesor
            {
                Id = profesorDTO.Id,
                Nombre = profesorDTO.Nombre,
                Especialidad = profesorDTO.Especialidad,
                Salario = profesorDTO.Salario,
                Experiencia = profesorDTO.Experiencia,
                Certificado = profesorDTO.Certificado,
                Contrato = profesorDTO.Contrato,
                Email = profesorDTO.Email
            };

            await _profesorRepository.UpdateAsync(profesor);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            await _profesorRepository.DeleteAsync(id);
        }
    }
}