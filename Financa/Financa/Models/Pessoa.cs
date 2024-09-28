using Dapper;
using Financa.ConnectionSqls;

namespace Financa.Models
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        private string CPF { get; set; }
        private float Salario { get; set; }

        public Pessoa() { }

        public Pessoa(string nome, string sobreNome, string cpf, float salario)
        {
            Nome = nome;
            Sobrenome = sobreNome;
            CPF = cpf;
            Salario = salario;
        }

        public Pessoa (string cpf)
        {
            CPF = cpf;
            Salario = ObterSalarioPeloCPF(cpf);
            Nome = ObterNomePeloCPF(cpf);
            Sobrenome = ObterSobrenomePeloCPF (cpf);
        }
        private float ObterSalarioPeloCPF(string cpf)
        {
                using (var connection = ConnectionSql.GetConnection())
                {
                    connection.Open();
                    string sql = "SELECT Salario from Pessoa WHERE CPF = @cpf";
                    var salario = connection.QuerySingleOrDefault<float>(sql,new { CPF = cpf });
                    return salario;
                }
        }

        private string ObterNomePeloCPF(string cpf)
        {
            using (var connection = ConnectionSql.GetConnection())
            {
                connection.Open();
                string sql = "SELECT Nome from Pessoa WHERE CPF = @cpf";
                var nome = connection.QuerySingleOrDefault<string>(sql, new { CPF = cpf });
                return nome;
            }
        }

        private string ObterSobrenomePeloCPF(string cpf)
        {
            using (var connection = ConnectionSql.GetConnection())
            {
                connection.Open();
                string sql = "SELECT Sobrenome from Pessoa WHERE CPF = @cpf";
                var sobrenome = connection.QuerySingleOrDefault<string>(sql, new { CPF = cpf });
                return sobrenome;
            }
        }

        public class CpfNaoCadastradoException : Exception
        {
            public CpfNaoCadastradoException(string cpf)
                : base($"CPF '{cpf}' não cadastrado.") { }
        }

    }
}
