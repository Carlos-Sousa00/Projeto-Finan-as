using Dapper;
using Financa.ConnectionSqls;
using Financa.Models;

namespace Financa.Repositories
{
    public class ConsultaPessoaPorCpfRepository
    {
        public async Task<Pessoa> GetPessoaAsync(string cpf)
        {
            using (var connection = ConnectionSql.GetConnection())
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstOrDefaultAsync<Pessoa>(
                    "SELECT Nome, Sobrenome, CPF, Salario FROM Pessoa WHERE CPF = @cpf", new { cpf });

                if (result == null)
                {
                    throw new Pessoa.CpfNaoCadastradoException(cpf);
                }

                return result;
            }
        }
    }
}
