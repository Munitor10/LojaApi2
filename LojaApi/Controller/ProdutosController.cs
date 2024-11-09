using LojaApi.Models;
using LojaApi.Repositorys;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace LojaApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutosRepositorys _produtosRepository;

        public ProdutosController(ProdutosRepositorys produtosRepository)
        {
            _produtosRepository = produtosRepository;
        }


        // GET api/<BlibiotecaControllers>/5
        [HttpPost("Listar-produtos")]
        public async Task<IActionResult> RegistrarProduto([FromBody] Produto produtos)
        {
            var Produtos = await _produtosRepository.RegistrarProdutoDB();
            return Ok(Produtos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] Produto produtos)
        {
            produtos.Id = id;
            await _produtosRepository.AtualizarProduto(produtos);
            return Ok(new { mensagem = "produto atualizado" });
        }

        [HttpDelete("excluir-produto/{id}")]
        public async Task<IActionResult> ExcluirProduto(int id)
        {
            try
            {
                await _produtosRepository.ExcluirProduto(id);

                return Ok(new { mensagem = "Produto excluído!" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}

