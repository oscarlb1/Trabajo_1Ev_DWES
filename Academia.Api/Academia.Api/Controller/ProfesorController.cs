using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;
using Academia.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorService _profesorService;

        public ProfesorController(IProfesorService profesorService)
        {
            _profesorService = profesorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProfesorDTO>>> GetProfesores([FromQuery] ProfesorParameters parameters)
        {
            var profesores = await _profesorService.GetAllAsync(parameters);

            if (profesores == null || profesores.Count == 0)
            {
                return NotFound();
            }

            return Ok(profesores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesorDTO>> GetProfesor(int id)
        {
            var profesor = await _profesorService.GetByIdAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }

            return Ok(profesor);
        }

        [HttpPost]
        public async Task<ActionResult<ProfesorDTO>> CreateProfesor(ProfesorCreateDTO profesorCreateDTO)
        {
            var newProfesor = await _profesorService.AddAsync(profesorCreateDTO);
            return CreatedAtAction(nameof(GetProfesor), new { id = newProfesor.Id }, newProfesor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfesor(int id, ProfesorUpdateDTO updatedProfesor)
        {
            var existingProfesor = await _profesorService.GetByIdAsync(id);
            if (existingProfesor == null)
            {
                return NotFound();
            }

            var profesorDtoToUpdate = new ProfesorDTO
            {
                Id = id,
                Nombre = updatedProfesor.Nombre,
                Especialidad = updatedProfesor.Especialidad,
                Salario = updatedProfesor.Salario,
                Experiencia = updatedProfesor.Experiencia,
                Certificado = updatedProfesor.Certificado,
                Contrato = updatedProfesor.Contrato,
                Email = updatedProfesor.Email
            };

            await _profesorService.UpdateAsync(profesorDtoToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _profesorService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            await _profesorService.DeleteAsync(id);
            return NoContent();
        }
    }
}