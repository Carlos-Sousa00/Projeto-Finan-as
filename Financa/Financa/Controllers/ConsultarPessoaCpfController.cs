using Financa.Models;
using Financa.Repositories;
using Financa.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Financa.Models.Pessoa;

namespace Financa.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ConsultarPessoaCpfController : ControllerBase
    {
        private readonly ConsultaPessoaPorCpfRepository _pessoaRepository;

        public ConsultarPessoaCpfController(ConsultaPessoaPorCpfRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetPessoaPorCpf(string cpf)
        {   
            cpf = cpf.Replace("-","").Replace(".", "");

            try
            {
                var pessoa = await _pessoaRepository.GetPessoaAsync(cpf);


                var pessoaCpfViewModel = new PessoaCpfViewModel
                {
                    Nome = pessoa.Nome,
                    Sobrenome = pessoa.Sobrenome,
                    CPF = pessoa.GetCpf(),  
                    Salario = pessoa.GetSalario()  
                };

                return Ok(pessoaCpfViewModel);
            }
            catch (Pessoa.CpfNaoCadastradoException)
            {
                return NotFound(new { Mensagem = $"CPF {cpf} não cadastrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro interno no servidor.", Detalhes = ex.Message });
            }
        }

    }
}
