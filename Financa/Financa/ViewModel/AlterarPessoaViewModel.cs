using System.ComponentModel.DataAnnotations;

namespace Financa.ViewModel
{
    public class AlterarPessoaViewModel
    {
        [Required]
        public string cpf { get; set; }

        [Required]
        public float salario { get; set; }
    }
}
