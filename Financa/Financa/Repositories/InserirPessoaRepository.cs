using Dapper;
using Financa.ConnectionSqls;
using Financa.Models;
using System.Data.SqlClient;
using System;
using DocumentValidator;

namespace Financa.Repositories
{
    public class InserirPessoaRepository
    {
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
    }
}
