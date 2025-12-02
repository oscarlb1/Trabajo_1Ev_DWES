using Academia.Api.Models.DTO;
using Academia.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CursoDTO>>> GetCursos()
        {
            var cursos = await _cursoService.GetAllAsync();
            return Ok(cursos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDTO>> GetCurso(int id)
        {
            var curso = await _cursoService.GetByIdAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            return Ok(curso);
        }

        [HttpPost]
        public async Task<ActionResult<CursoDTO>> CreateCurso(CursoCreateDTO cursoCreateDTO)
        {
            var newCurso = await _cursoService.AddAsync(cursoCreateDTO);
            return CreatedAtAction(nameof(GetCurso), new { id = newCurso.Id }, newCurso);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurso(int id, CursoUpdateDTO updatedCurso)
        {
            var existingCurso = await _cursoService.GetByIdAsync(id);
            if (existingCurso == null)
            {
                return NotFound();
            }

            var cursoDtoToUpdate = new CursoDTO
            {
                Id = id,
                Titulo = updatedCurso.Titulo,
                Detalle = updatedCurso.Detalle,
                Costo = updatedCurso.Costo,
                Horas = updatedCurso.Horas,
                Publicado = updatedCurso.Publicado,
                Creacion = updatedCurso.Creacion,
                ProfesorId = updatedCurso.ProfesorId,
                MateriaId = updatedCurso.MateriaId
            };

            await _cursoService.UpdateAsync(cursoDtoToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var curso = await _cursoService.GetByIdAsync(id);
            if (curso == null)
            {
                return NotFound();
            }

            await _cursoService.DeleteAsync(id);
            return NoContent();
        }
    }
}