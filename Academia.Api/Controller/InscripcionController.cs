using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;
using Academia.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionController : ControllerBase
    {
        private readonly IInscripcionService _inscripcionService;

        public InscripcionController(IInscripcionService inscripcionService)
        {
            _inscripcionService = inscripcionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<InscripcionDTO>>> GetInscripciones([FromQuery] InscripcionParameters parameters)
        {
            var inscripciones = await _inscripcionService.GetAllAsync(parameters);
            return Ok(inscripciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InscripcionDTO>> GetInscripcion(int id)
        {
            var inscripcion = await _inscripcionService.GetByIdAsync(id);
            if (inscripcion == null)
            {
                return NotFound();
            }

            return Ok(inscripcion);
        }

        [HttpPost]
        public async Task<ActionResult<InscripcionDTO>> CreateInscripcion(InscripcionCreateDTO inscripcionCreateDTO)
        {
            var newInscripcion = await _inscripcionService.AddAsync(inscripcionCreateDTO);
            return CreatedAtAction(nameof(GetInscripcion), new { id = newInscripcion.Id }, newInscripcion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInscripcion(int id, InscripcionUpdateDTO updatedInscripcion)
        {
            var existingInscripcion = await _inscripcionService.GetByIdAsync(id);
            if (existingInscripcion == null)
            {
                return NotFound();
            }

            var inscripcionDtoToUpdate = new InscripcionDTO
            {
                Id = id,
                Progreso = updatedInscripcion.Progreso,
                Comentario = updatedInscripcion.Comentario,
                Nota = updatedInscripcion.Nota,
                Activa = updatedInscripcion.Activa,
                InscripcionFecha = updatedInscripcion.InscripcionFecha,
                UsuarioId = updatedInscripcion.UsuarioId,
                CursoId = updatedInscripcion.CursoId
            };

            await _inscripcionService.UpdateAsync(inscripcionDtoToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscripcion(int id)
        {
            var inscripcion = await _inscripcionService.GetByIdAsync(id);
            if (inscripcion == null)
            {
                return NotFound();
            }

            await _inscripcionService.DeleteAsync(id);
            return NoContent();
        }
    }
}