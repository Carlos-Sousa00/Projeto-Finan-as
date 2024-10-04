using Financa.Models;
using Financa.Repositories;
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
        public IActionResult InserirPessoa([FromBody] PessoaDto pessoa)
        {
            try
            {
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
