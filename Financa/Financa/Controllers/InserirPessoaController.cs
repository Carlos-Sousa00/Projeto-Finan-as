using Financa.Models;
using Financa.Repositories;
using Financa.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Financa.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class InserirPessoaController : ControllerBase
    {
        private readonly InserirPessoaRepository _inserirPessoaRepository;

        public InserirPessoaController(InserirPessoaRepository inserirPessoaRepository)
        {
            _inserirPessoaRepository = inserirPessoaRepository;
        }

        [HttpPost]
        public IActionResult InserirPessoa([FromBody] PessoaCpfViewModel pessoaCpfViewModel)
        {
            try
            {
                var pessoa = new Pessoa(
                pessoaCpfViewModel.Nome,
                pessoaCpfViewModel.Sobrenome,
                pessoaCpfViewModel.CPF,
                pessoaCpfViewModel.Salario
                );
                _inserirPessoaRepository.InserirPessoa(pessoa);
                return Ok("Pessoa inserida com sucesso.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
