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
    }
}
