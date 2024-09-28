using Dapper;
using Financa.ConnectionSqls;

namespace Financa.Models
{
    public class PessoaDto
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public float Salario { get; set; }
    }

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

        public PessoaDto ToDto()
        {
            return new PessoaDto
            {
                Nome = this.Nome,
                Sobrenome = this.Sobrenome,
                CPF = this.CPF,
                Salario = this.Salario
            };
        }

        public class CpfNaoCadastradoException : Exception
        {
            public CpfNaoCadastradoException(string cpf)
                : base($"CPF '{cpf}' não cadastrado.") { }
        }
    }
}
