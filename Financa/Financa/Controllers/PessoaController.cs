using Microsoft.AspNetCore.Mvc;
using Financa.Repositories;  // Namespace do repositório
using Financa.Models;       // Namespace do modelo
using System.Collections.Generic;
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
    public async Task<ActionResult<IEnumerable<Pessoa>>> Get()
    {
        var pessoas = await _pessoaRepository.GetAllPessoaAsync();
        return Ok(pessoas);
    }
}
