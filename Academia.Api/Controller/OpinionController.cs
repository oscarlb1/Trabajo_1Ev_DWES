using Academia.Api.Models.DTO;
using Academia.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpinionController : ControllerBase
    {
        private readonly IOpinionService _opinionService;

        public OpinionController(IOpinionService opinionService)
        {
            _opinionService = opinionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OpinionDTO>>> GetOpiniones()
        {
            var opiniones = await _opinionService.GetAllAsync();
            return Ok(opiniones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OpinionDTO>> GetOpinion(int id)
        {
            var opinion = await _opinionService.GetByIdAsync(id);
            if (opinion == null)
            {
                return NotFound();
            }

            return Ok(opinion);
        }

        [HttpPost]
        public async Task<ActionResult<OpinionDTO>> CreateOpinion(OpinionCreateDTO opinionCreateDTO)
        {
            var newOpinion = await _opinionService.AddAsync(opinionCreateDTO);
            return CreatedAtAction(nameof(GetOpinion), new { id = newOpinion.Id }, newOpinion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOpinion(int id, OpinionUpdateDTO updatedOpinion)
        {
            var existingOpinion = await _opinionService.GetByIdAsync(id);
            if (existingOpinion == null)
            {
                return NotFound();
            }

            var opinionDtoToUpdate = new OpinionDTO
            {
                Id = id,
                Nombre = updatedOpinion.Nombre,
                FechaComentario = updatedOpinion.FechaComentario,
                Mensaje = updatedOpinion.Mensaje,
                Puntuacion = updatedOpinion.Puntuacion,
                CursoId = updatedOpinion.CursoId
            };

            await _opinionService.UpdateAsync(opinionDtoToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOpinion(int id)
        {
            var opinion = await _opinionService.GetByIdAsync(id);
            if (opinion == null)
            {
                return NotFound();
            }

            await _opinionService.DeleteAsync(id);
            return NoContent();
        }
    }
}