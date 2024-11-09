using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using LojaApi.Models;

namespace LojaApi.Repositorys
{
    public class UsuarioReposirys
    {
        private readonly string _connectionString;

        public UsuarioReposirys(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<int> RegistrarUsuario(Usuarios usuarios)
        {
            using (var conn = Connection)
            {
                var sql = "INSERT INTO Usuarios (Nome, Email, Endereco) VALUES (@Nome, @Email, @Endereco);" +
                          "SELECT LAST_INSERT_ID();";
                return await conn.ExecuteScalarAsync<int>(sql, usuarios);
            }
        }

        public async Task<IEnumerable<Usuarios>> ListarUsuariosDB()
        {
            using (var conn = Connection)
            {
                var sql = "SELECT * FROM Usuarios";
                return await conn.QueryAsync<Usuarios>(sql);
            }
        }

    }

}
