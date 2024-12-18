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
    public ActionResult<Saida> Create(Saida saida)
    {
        try
        {
            if (saida == null)
            {
                return BadRequest();
            }

            var saidaAdicionada = _repository.Add(saida);
            return Ok(saidaAdicionada);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }        
    }

    [HttpGet]
    public ActionResult<IEnumerable<Saida>> GetAll()
    {
        try
        {
            var saidas = _repository.GetAll();            
            return Ok(saidas);
        }
        catch (Exception ex)
        {
            return NotFound($"{ex.Message}");
        }
        
    }

    [HttpPut]
    public ActionResult<Saida> Update(Saida saida)
    {
        try
        {
            return Ok(_repository.Update(saida));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try
        {
            _repository.Remove(id);
            return Ok($"A saida com id: {id} foi deletada com sucesso");
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
