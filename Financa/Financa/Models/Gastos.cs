namespace Financa.Models
{
    public class Gastos
    {
        public int Tipo { get; set; }
        private float Valor { get; set; }
        public string Descricao { get; set; }
        public bool Mensal { get; set; }
        public DateTime  DataCadastro { get; set; }
        private string CPF { get; set; }

        public Gastos(int tipo,float valor,string descricao, string cpf) 
        { 
            Tipo = tipo;
            Valor = valor;
            Descricao = descricao;
            CPF = cpf;
        }
    }
}
