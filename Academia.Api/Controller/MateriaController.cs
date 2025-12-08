using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;
using Academia.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaService _materiaService;

        public MateriaController(IMateriaService materiaService)
        {
            _materiaService = materiaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MateriaDTO>>> GetMaterias([FromQuery] MateriaParameters parameters)
        {
            var materias = await _materiaService.GetAllAsync(parameters);
            return Ok(materias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaDTO>> GetMateria(int id)
        {
            var materia = await _materiaService.GetByIdAsync(id);
            if (materia == null)
            {
                return NotFound();
            }

            return Ok(materia);
        }

        [HttpPost]
        public async Task<ActionResult<MateriaDTO>> CreateMateria(MateriaCreateDTO materiaCreateDTO)
        {
            var newMateria = await _materiaService.AddAsync(materiaCreateDTO);
            return CreatedAtAction(nameof(GetMateria), new { id = newMateria.Id }, newMateria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMateria(int id, MateriaCreateDTO updatedMateria)
        {
            var existingMateria = await _materiaService.GetByIdAsync(id);
            if (existingMateria == null)
            {
                return NotFound();
            }

            var materiaDtoToUpdate = new MateriaDTO
            {
                Id = id,
                Nombre = updatedMateria.Nombre,
                Detalle = updatedMateria.Detalle,
                Nivel = updatedMateria.Nivel,
                Cantidad = updatedMateria.Cantidad,
                Obligatoria = updatedMateria.Obligatoria,
                Creacion = updatedMateria.Creacion
            };

            await _materiaService.UpdateAsync(materiaDtoToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria(int id)
        {
            var materia = await _materiaService.GetByIdAsync(id);
            if (materia == null)
            {
                return NotFound();
            }

            await _materiaService.DeleteAsync(id);
            return NoContent();
        }
    }
}