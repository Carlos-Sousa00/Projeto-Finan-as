using Microsoft.AspNetCore.Mvc;
using Financa.Repositories;
using Financa.Models;
using Financa.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


[ApiController]
[Route("[controller]")]
public class PessoasController : ControllerBase
{
    private readonly PessoaRepository _pessoaRepository;

    public PessoasController(PessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IList<PessoaViewModel>>> Get()
    {
        var pessoas = await _pessoaRepository.GetAllPessoaAsync();

        var pessoasViewModel = pessoas.Select(p => new PessoaViewModel
        {
            Nome = p.Nome,
            Sobrenome = p.Sobrenome,
        }).ToList();

        return Ok(pessoasViewModel);
    }


    [HttpGet("{cpf}")]
    public async Task<IActionResult> GetPessoaPorCpf(string cpf)
    {
        cpf = cpf.Replace("-", "").Replace(".", "");

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
            _pessoaRepository.InserirPessoa(pessoa);
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
    [HttpDelete("{cpf}")]
    public IActionResult DeletePessoa(string cpf)
    {
        try
        {
            var pessoa = new Pessoa();

            _pessoaRepository.DeletarPessoa(cpf);

            return Ok("Pessoa excluida com sucesso!");
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Ocorreu um erro ao excluir a pessoa.", Details = ex.Message });
        }
    }
    [HttpPut]
    public IActionResult AlteraPessoa([FromBody] AlterarPessoaViewModel view )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            _pessoaRepository.AlterarPessoa(view.cpf, view.salario);
            return Ok($"Salário alterado com sucesso para {view.salario}.");
        }
        catch(InvalidOperationException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Ocorreu um erro ao alterar o salário pessoa.", Details = ex.Message });
        }
    }
}
