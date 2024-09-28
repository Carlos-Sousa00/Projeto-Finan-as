using Dapper;
using Financa.ConnectionSqls;
using Financa.Models;
using static Financa.Models.Pessoa;

namespace Financa.Repositories
{
    public class ConsultaPessoaPorCpfRepository
    {
        public async Task<Pessoa> GetPessoaAsync(string cpf)
        {
            using (var connection = ConnectionSql.GetConnection())
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstOrDefaultAsync(
                    "SELECT Nome, Sobrenome, CPF, Salario FROM Pessoa WHERE CPF = @cpf", new { cpf });

                if (result == null)
                {
                    throw new CpfNaoCadastradoException(cpf);
                }

                var pessoa = new Pessoa(result.CPF);
                return pessoa;
            }
        }

    }
}
