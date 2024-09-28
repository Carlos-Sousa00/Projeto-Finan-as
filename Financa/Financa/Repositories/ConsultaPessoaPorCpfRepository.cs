using Dapper;
using Financa.ConnectionSqls;
using Financa.Models;

namespace Financa.Repositories
{
    public class ConsultaPessoaPorCpfRepository
    {
        public async Task<PessoaDto> GetPessoaAsync(string cpf)
        {
            using (var connection = ConnectionSql.GetConnection())
            {
                await connection.OpenAsync();
                var result = await connection.QueryFirstOrDefaultAsync<PessoaDto>(
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
