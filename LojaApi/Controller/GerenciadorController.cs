using LojaApi.Repositorys;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LojaApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GerenciadorController : ControllerBase
    {
        private readonly GerenciamentoRepository _gerenciamentoRepository;

        public GerenciadorController(GerenciamentoRepository gerenciamentoRepository)
        {
            _gerenciamentoRepository = _gerenciamentoRepository;
        }

        // GET: api/<GerenciadorController>
        [HttpGet("lista")]
        public async Task<IActionResult> ListarPedidos()
        {
            var pedidos = await _gerenciamentoRepository.ListarPedidos();
            return Ok(pedidos);
        }

        // GET api/<GerenciadorController>/5
        [HttpGet("consultar-pedidos")]
        public async Task<IActionResult> ConsultarPedidos([FromQuery] string? statusPedido)
        {
            var pedido = await _gerenciamentoRepository.ConsultarPedidos(statusPedido);
            return Ok(pedido);
        }
    }
}
