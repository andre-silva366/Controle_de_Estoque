using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeAlmoxarifado.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class SaidaController : ControllerBase
{
    private readonly IRepository<Saida> _repository;
    private readonly ITransacoesRepository<Saida> _transRepository;

    public SaidaController(IRepository<Saida> repository, ITransacoesRepository<Saida> transRepository)
    {
        _repository = repository;
        _transRepository = transRepository;
    }

    [HttpPost]
    public IActionResult Create(Saida saida)
    {
        if (saida == null)
        {
            return BadRequest();
        }

        var saidaAdicionada = _repository.Add(saida);
        return Ok(saidaAdicionada);
    }
}
