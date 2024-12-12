using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeAlmoxarifado.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntradaController : ControllerBase
{
    private readonly IRepository<Entrada> _repository;
    private readonly ITransacoesRepository<Entrada> _transRepository;

    public EntradaController(IRepository<Entrada> repository, ITransacoesRepository<Entrada> transRepository)
    {
        _repository = repository;
        _transRepository = transRepository;
    }

    [HttpPost]
    public IActionResult Create([FromBody]Entrada entrada)
    {
        if(entrada == null)
        {
            return BadRequest();
        }

        var entradaAdicionada = _repository.Add(entrada);
        return Ok(entradaAdicionada);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Entrada>> GetAll()
    {
        try
        {
            var entradas = _repository.GetAll().ToList();
            return Ok(entradas);
        }
        catch(Exception ex)
        {
            return NotFound($"{ex.Message}");
        }
        
    }
}
