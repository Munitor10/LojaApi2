using Dapper;
using LijaApi.Controllers;
using MySql.Data.MySqlClient;
using System.Data;

namespace LojaApi.Repositorys
{
    public class GerenciamentoRepository
    {
        private readonly string _connectionString;
        public GerenciamentoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<IEnumerable<Pedidos>> ListarPedidos()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM db_aulas_2024.Pedidos";
                return await conn.QueryAsync<Pedidos>(sql);
            }
        }

        public async Task<IEnumerable<Pedidos>> ConsultarPedidos(string? statusPedido = null)
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Pedidos WHERE 1=1";

                if (!string.IsNullOrEmpty(statusPedido))
                {
                    sql += " AND StatusPedido = @StatusPedido";
                }

                return await conn.QueryAsync<Pedidos>(sql, new {  StatusPedido = statusPedido });
            }
        }
    }
}
