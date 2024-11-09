using Dapper;
using LojaApi.Models;
using MySql.Data.MySqlClient;
using System.Data;


namespace LojaApi.Repositorys
{
    public class ProdutosRepositorys
    {
        private readonly string _connectionString;

        public ProdutosRepositorys(string connectionString)
        {
            _connectionString = connectionString;
        }


        private IDbConnection Connection => new MySqlConnection(_connectionString);


        public async Task<IEnumerable<Produto>> ListarProdutosDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Produtos";
                return await conn.QueryAsync<Produto>(sql);
            }
        }


        public async Task<int> RegistrarProduto(Produto produtos)
        {
            using (var conn = Connection)
            {
                var sql = "INSERT INTO Produtos (Nome, Dercricao, Preco, QuantidadeEstoque) VALUES (@Nome, @Dercricao, @Preco, @QuantidadeEstoque);SELECT LAST_INSERT_ID();";
                return await conn.ExecuteScalarAsync<int>(sql, produtos);
            }
        }

        public async Task<int> AtualizarProduto(Produto produtos)
        {
            using (var conn = Connection)
            {
                var sql = "UPDATE Produtos SET Nome = @Nome, Descricao = @Descricao, Preco = @Preco, " +
                          "QuantidadeEstoque = @QuantidadeEstoque";
                return await conn.ExecuteAsync(sql, produtos);
            }
        }


        public async Task<int> ExcluirProduto(int id)
        {
            using (var conn = Connection)
            {
                var sql = "DELETE FROM Produtos WHERE Id = @Id";
                return await conn.ExecuteAsync(sql, new { Id = id });
            }
        }

    }
}
