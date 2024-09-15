namespace Financa.Models
{
    public class CalculoGastos
    {
        private string CPF { get; set; }
        private List<float> Gastos { get; set; }
        private float SalarioCalculado { get; }

        public CalculoGastos(string cpf,float salarioCalculado)
        {
            CPF = cpf;
            SalarioCalculado = salarioCalculado;
        }
        public void AdicionarGastos (float gasto)
        {
            Gastos.Add(gasto);
        }

    }

 
}
