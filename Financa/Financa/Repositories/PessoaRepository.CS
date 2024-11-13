using Dapper;
using DocumentValidator;
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
        public void InserirPessoa(Pessoa pessoa)
        {
            using (var connection = ConnectionSql.GetConnection())
            {

                if (CpfValidation.Validate(pessoa.GetCpf()))
                {
                    string sql = @"INSERT INTO Pessoa (Nome, Sobrenome, CPF, SALARIO)
                    VALUES (@Nome, @Sobrenome, REPLACE(REPLACE(@Cpf,'-',''),'.',''), @Salario)";

                    connection.Execute(sql, new { pessoa.Nome, pessoa.Sobrenome, Cpf = pessoa.GetCpf(), Salario = pessoa.GetSalario() });
                }
                else
                {
                    throw new ArgumentException("CPF inválido.");
                }
            }
        }
        public void DeletarPessoa (string cpf)
        {
            using (var connection = ConnectionSql.GetConnection())
            {
                var pessoa = new Pessoa(cpf);
                var pessoaExistente = connection.QueryFirstOrDefault<Pessoa>("SELECT * FROM Pessoa WHERE CPF = @Cpf", new {Cpf = pessoa.GetCpf()});
                if (pessoaExistente != null)
                {
                    string sql = @"DELETE PESSOA WHERE CPF = @Cpf";

                    connection.Execute(sql, new { Cpf = pessoa.GetCpf() });
                }
                else
                {
                    throw new InvalidOperationException("Pessoa com o CPF informado não foi encontrada.");
                }
            }
        }
        public void AlterarPessoa(string cpf, float salario)
        {
            using (var connection = ConnectionSql.GetConnection())
            {
                var pessoa = new Pessoa(cpf,salario);
                var pessoaExistente = connection.QueryFirstOrDefault<Pessoa>("SELECT * FROM Pessoa WHERE CPF = @Cpf", new { Cpf = pessoa.GetCpf() });
                if (pessoaExistente != null)
                {
                    string sql = $"UPDATE PESSOA SET SALARIO = {pessoa.GetSalario()} WHERE CPF = '{pessoa.GetCpf()}'";

                    connection.Execute(sql);
                }
                else
                {
                    throw new InvalidOperationException("Pessoa com o CPF informado não foi encontrada.");
                }
            }
        }

    }

}
