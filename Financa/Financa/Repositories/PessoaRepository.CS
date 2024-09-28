using Dapper;
using Financa.ConnectionSqls;
using Financa.Models;

namespace Financa.Repositories
{
    public class PessoaRepository
    {
        public async Task<IList<Pessoa>> GetAllPessoaAsync()
        {
            using (var connection = ConnectionSql.GetConnection())
            {
                await connection.OpenAsync();
                var pessoas = await connection.QueryAsync<Pessoa>(
                    "SELECT Nome, Sobrenome FROM Pessoa");
                return pessoas.ToList();
            }
        }
    }
}
