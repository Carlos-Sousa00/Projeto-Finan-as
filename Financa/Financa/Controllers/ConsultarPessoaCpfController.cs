using Financa.Models;
using Financa.Repositories;
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
            try
            {
                var pessoa = await _pessoaRepository.GetPessoaAsync(cpf);
                return Ok(pessoa);
            }
            catch (CpfNaoCadastradoException)
            {
                return NotFound(new { Mensagem = "CPF não cadastrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensagem = "Erro interno no servidor.", Detalhes = ex.Message });
            }
        }
    }
}
