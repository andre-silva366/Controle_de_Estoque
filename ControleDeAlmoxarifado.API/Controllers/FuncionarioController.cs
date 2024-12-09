using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeAlmoxarifado.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FuncionarioController : ControllerBase
{
    private readonly IRepository<Funcionario> _repository;

    public FuncionarioController(IRepository<Funcionario> repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult Create([FromBody]Funcionario funcionario)
    {
        if (funcionario == null)
        {
            return BadRequest();
        }
        return Ok(_repository.Add(funcionario));
    }

    [HttpGet]
    public ActionResult<IEnumerable<Funcionario>> GetAll()
    {
        var funcionarios = _repository.GetAll().ToList();
        if(!funcionarios.Any())
        {
            return NoContent();
        }
        return funcionarios;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var funcionario = _repository.GetById(id);
        if(funcionario == null)
        {
            return BadRequest($"Não foi encontrado funcionario com id: {id}"); ;
        }
        return Ok(funcionario);
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        try
        {
            _repository.Remove(id);
            return Ok($"O funcionario com id: {id} foi deletado com sucesso.");
        }
        catch (Exception ex)
        {
            return BadRequest($"{ex.Message}");
        }
    }
}
