using Microsoft.AspNetCore.Mvc;
using Financa.Repositories;  
using Financa.Models;       
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
    public async Task<ActionResult<IList<Pessoa>>> Get()
    {
        var pessoas = await _pessoaRepository.GetAllPessoaAsync();
        return Ok(pessoas);
    }
}
