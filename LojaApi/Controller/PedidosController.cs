using LojaApi.Repositorys;
using Microsoft.AspNetCore.Mvc;

namespace LojaApi.Controller
{
    [Route("api/pedido")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoRepository _pedidoRepository;

        public PedidoController(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
    
        [HttpGet("listar-pedidos")]
        public async Task<IActionResult> ListarPedidosUsuario(int usuarioId)
        {
            var pedidos = await _pedidoRepository.ListarPedidosUsuarioDB(usuarioId);

            return Ok(pedidos);
        }

        [HttpGet("consultar-status")]
        public async Task<IActionResult> ConsultarStatusPedido(int pedidoId)
        {
            var status = await _pedidoRepository.ConsultarStatusPedidoDB(pedidoId);

            return Ok(new { pedidoId, status });
        }


    }
}
