using Academia.Api.Models;
using Academia.Api.Models.DTO;
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

        public async Task<List<ProfesorDTO>> GetAllAsync()
        {
            var profesores = await _profesorRepository.GetAllAsync();

            return profesores.Select(u => new ProfesorDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Especialidad = u.Especialidad,
                Salario = u.Salario,
                Experiencia = u.Experiencia,
                Certificado = u.Certificado,
                Contrato = u.Contrato,
                Email = u.Email
            }).ToList();
        }

        public async Task<ProfesorDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor que cero.");

            var usuario = await _profesorRepository.GetByIdAsync(id);

            if (usuario == null)
                return null;

            return new ProfesorDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Especialidad = usuario.Especialidad,
                Salario = usuario.Salario,
                Experiencia = usuario.Experiencia,
                Certificado = usuario.Certificado,
                Contrato = usuario.Contrato,
                Email = usuario.Email
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