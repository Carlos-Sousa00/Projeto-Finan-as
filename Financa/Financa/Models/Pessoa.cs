namespace Financa.Models
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        private string CPF { get; set; }
        private float Salario { get; set; }

        public Pessoa() { }

        public Pessoa(string cpf)
        {
            CPF = cpf;
        }

        public Pessoa(string cpf,float salario)
        {
            CPF = cpf;
            Salario = salario;
        }

        public Pessoa(string nome, string sobreNome, string cpf, float salario)
        {
            Nome = nome;
            Sobrenome = sobreNome;
            CPF = cpf;
            Salario = salario;
        }

        public string GetCpf()
        {
            return CPF;
        }

        public float GetSalario()
        {
            return Salario;
        }

        public class CpfNaoCadastradoException : Exception
        {
            public CpfNaoCadastradoException(string cpf)
                : base($"CPF '{cpf}' não cadastrado.") { }
        }
    }
}
