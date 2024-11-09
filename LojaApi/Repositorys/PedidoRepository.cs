using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaApi.Repositorys
{
    public class PedidoRepository
    {
        private readonly string _connectionString;
        private readonly CarrinhoRepositorys _carrinhoRepository;

        public PedidoRepository(string connectionString, CarrinhoRepositorys carrinhoRepository)
        {
            _connectionString = connectionString;
            _carrinhoRepository = carrinhoRepository;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<IEnumerable<dynamic>> ListarPedidosUsuarioDB(int usuarioId)
        {
            using (var conn = Connection)
            {
                var sql = @"SELECT p.Id, p.DataPedido, p.StatusPedido, p.ValorTotal, 
                                   pp.ProdutoId, pr.Nome, pr.Descricao, pp.Quantidade, pp.Preco
                            FROM Pedidos p
                            JOIN PedidoProdutos pp ON p.Id = pp.PedidoId
                            JOIN Produtos pr ON pp.ProdutoId = pr.Id
                            WHERE p.UsuarioId = @UsuarioId
                            ORDER BY p.DataPedido DESC";

                return await conn.QueryAsync<dynamic>(sql, new { UsuarioId = usuarioId });
            }
        }

        public async Task<string> ConsultarStatusPedidoDB(int pedidoId)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT StatusPedido FROM Pedidos WHERE Id = @PedidoId";

                return await conn.ExecuteScalarAsync<string>(sql, new { PedidoId = pedidoId });
            }
        }

        //Criarr um novo pedido lendo os itens do do carrinho
        public async Task<int> CriarPedidoDB(int usuarioId)
        {
            using (var conn = Connection)
            {
                using (var transaction = conn.BeginTransaction())
                {
                    //li os itens do carrinho
                    var itensCarrinho = await _carrinhoRepository.ConsultarCarrinhoDB(usuarioId);

                    var valorTotal = await _carrinhoRepository.CalcularValorTotalCarrinho(usuarioId);

                    var sqlInserirPedido = "INSERT INTO Pedidos (UsuarioId, DataPedido, StatusPedido, ValorTotal) " +
                                           "VALUES (@UsuarioId, @DataPedido, @StatusPedido, @ValorTotal);" +
                                           "SELECT LAST_INSERT_ID();";

                    var pedidoId = await conn.ExecuteScalarAsync<int>(sqlInserirPedido, new
                    {
                        UsuarioId = usuarioId,
                        DataPedido = DateTime.Now,
                        ValorTotal = valorTotal
                    }, transaction);

                  //  var itensCarrinho = await _carrinhoRepository Cosutar

                    foreach (var item in itensCarrinho)
                    {
                        var sqlInserirPedidoProduto = "INSERT INTO PedidoProdutos (PedidoId, ProdutoId, Quantidade, Preco) " +
                                                      "VALUES (@PedidoId, @ProdutoId, @Quantidade, @Preco);";
                        await conn.ExecuteAsync(sqlInserirPedidoProduto, new
                        {
                            PedidoId = pedidoId,
                            item.ProdutoId,
                            item.Quantidade,
                            item.Preco
                        }, transaction);

                        //Atualização automática do estoque
                        var sqlAtualizaEstoque = "UPDATE Produtos SET QuantidadeEstoque = QuantidadeEstoque - @Quantidade " +
                                                  "WHERE Id = @ProdutoId AND QuantidadeEstoque >= @Quantidade";
                        var linhasAfetadas = await conn.ExecuteAsync(sqlAtualizaEstoque, new
                        {
                            ProdutoId = item.ProdutoId,
                            Quantidade = item.Quantidade
                        }, transaction);
                    }

                    var sqlLimparCarrinho = "DELETE FROM Carrinho WHERE UsuarioId = @UsuarioId";
                    await conn.ExecuteAsync(sqlLimparCarrinho, new { UsuarioId = usuarioId }, transaction);

                    transaction.Commit();
                    return pedidoId;
                }
            }
        }
    }
}
