using Academia.Api.Models.DTO;
using Academia.Api.Models.QueryParameters;
using Academia.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> GetUsuarios([FromQuery] UsuarioParameters parameters)
        {
            var usuarios = await _usuarioService.GetAllAsync(parameters);
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> CreateUsuario(UsuarioCreateDTO usuarioCreateDto)
        {
            var newUsuario = await _usuarioService.AddAsync(usuarioCreateDto);
            return CreatedAtAction(nameof(GetUsuario), new { id = newUsuario.Id }, newUsuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, UsuarioUpdateDTO updatedUsuario)
        {
            var existingUsuario = await _usuarioService.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                return NotFound();
            }

            var usuarioDtoToUpdate = new UsuarioDTO
            {
                Id = id,
                Nombre = updatedUsuario.Nombre,
                Email = updatedUsuario.Email,
                Creditos = updatedUsuario.Creditos,
                Cursos = updatedUsuario.Cursos,
                Premium = updatedUsuario.Premium,
                Registro = updatedUsuario.Registro
            };

            await _usuarioService.UpdateAsync(usuarioDtoToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            await _usuarioService.DeleteAsync(id);
            return NoContent();
        }
    }
}