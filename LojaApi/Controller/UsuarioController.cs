using LojaApi.Models;
using LojaApi.Repositorys;
using Microsoft.AspNetCore.Mvc;


namespace LojaApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioReposirys _usuarioRepository;

        public UsuarioController(UsuarioReposirys usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // GET api/<ValuesController>/5
        [HttpGet("lista-usuario")]
        public async Task<IActionResult> ListarUsuariosDB()
        {
            var usuario = await _usuarioRepository.ListarUsuariosDB();
            return Ok(usuario);
        }

        // POST api/<ValuesController>
        [HttpPost("registra-usuario")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] Usuarios usuarios)
        {
            var usuarioId = await _usuarioRepository.ListarUsuariosDB();
            return Ok(usuarioId);
        }

    }
}
