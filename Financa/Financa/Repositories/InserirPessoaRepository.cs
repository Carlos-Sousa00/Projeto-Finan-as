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
        public void InserirPessoa(PessoaDto pessoa)
        {
            using (var connection = ConnectionSql.GetConnection())
            {

                if (CpfValidation.Validate(pessoa.CPF))
                {
                    string sql = @"INSERT INTO Pessoa (Nome, Sobrenome, CPF, SALARIO)
                    VALUES (@Nome, @Sobrenome, REPLACE(REPLACE(@Cpf,'-',''),'.',''), @Salario)";
                    
                    connection.Execute(sql, new { pessoa.Nome, pessoa.Sobrenome, pessoa.CPF, pessoa.Salario });
                }
                else
                {
                    throw new ArgumentException("CPF inválido.");
                }
            }
        }
    }
}
