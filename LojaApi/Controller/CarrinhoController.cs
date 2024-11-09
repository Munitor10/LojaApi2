using LojaApi.Models;
using LojaApi.Repositorys;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LojaApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        private readonly CarrinhoRepositorys _carrinhoRepositorys;

        public CarrinhoController(CarrinhoRepositorys usuarioRepository)
        {
            _carrinhoRepositorys = usuarioRepository;
        }


        [HttpGet("listar")]
        public async Task<IActionResult> ConsultarCarrinho(int usuarioId)
        {
            var itens = await _carrinhoRepositorys.ConsultarCarrinhoDB(usuarioId);

            var valorTotal = await _carrinhoRepositorys.CalcularValorTotalCarrinho(usuarioId);

            return Ok(new { itens, valorTotal });
        }

        // POST api/<CarrinhoController>
        [HttpPost("Cadastra-item")]
        public async Task<IActionResult> RegistrarProduto([FromBody] Carrinho carrinho)
        {
            return Ok(carrinho);

        }

        // DELETE api/<CarrinhoController>/5
        [HttpDelete("Recebe-id-e-Deleta")]
        public async Task<IActionResult> ExcluirItem(int id)
        {
            try
            {
                await _carrinhoRepositorys.ExcluirItem(id);
                return Ok(new { mensagem = "item excruido" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("Atualiza-item")]
        public async Task<IActionResult> AtualizarItem(int id, [FromBody] Carrinho carrinho)
        {
            carrinho.Id = id;
            await _carrinhoRepositorys.AtualizarLivroDB(carrinho);

            return Ok(new { mensagem = "Item atualizado" });
        }

    }
}
