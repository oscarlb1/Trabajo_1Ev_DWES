using Academia.Api.Models.DTO;
using Academia.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeccionController : ControllerBase
    {
        private readonly ILeccionService _leccionService;

        public LeccionController(ILeccionService leccionService)
        {
            _leccionService = leccionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeccionDTO>>> GetLecciones()
        {
            var lecciones = await _leccionService.GetAllAsync();
            return Ok(lecciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeccionDTO>> GetLeccion(int id)
        {
            var leccion = await _leccionService.GetByIdAsync(id);
            if (leccion == null)
            {
                return NotFound();
            }

            return Ok(leccion);
        }

        [HttpPost]
        public async Task<ActionResult<LeccionDTO>> CreateLeccion(LeccionCreateDTO leccionCreateDTO)
        {
            await _leccionService.AddAsync(leccionCreateDTO);
            return CreatedAtAction(nameof(GetLeccion), new { id = leccionCreateDTO }, leccionCreateDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeccion(int id, LeccionUpdateDTO updatedLeccion)
        {
            var existingLeccion = await _leccionService.GetByIdAsync(id);
            if (existingLeccion == null)
            {
                return NotFound();
            }

            var leccionDtoToUpdate = new LeccionDTO
            {
                Id = id,
                Titulo = updatedLeccion.Titulo,
                Tipo = updatedLeccion.Tipo,
                Minutos = updatedLeccion.Minutos,
                Examen = updatedLeccion.Examen,
                Publicacion = updatedLeccion.Publicacion,
                URL = updatedLeccion.URL,
                CursoId = updatedLeccion.CursoId
            };

            await _leccionService.UpdateAsync(leccionDtoToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeccion(int id)
        {
            var leccion = await _leccionService.GetByIdAsync(id);
            if (leccion == null)
            {
                return NotFound();
            }

            await _leccionService.DeleteAsync(id);
            return NoContent();
        }
    }
}