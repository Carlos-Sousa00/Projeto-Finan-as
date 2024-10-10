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
}
