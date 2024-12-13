using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeAlmoxarifado.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class CategoriaController : ControllerBase
{
    private readonly IRepository<Categoria> _repository;

    public CategoriaController(IRepository<Categoria> repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult Create([FromBody] Categoria categoria)
    {
        if(categoria == null)
        {
            return BadRequest();
        }
        return Ok(_repository.Add(categoria));
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> GetAll()
    {
        var categorias = _repository.GetAll().ToList();
        if(!categorias.Any())
        {
            return NoContent();
        }
        return categorias;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Categoria> GetById(int id)
    {
        try
        {
            var categoria = _repository.GetById(id);
            if (categoria == null)
            {
                return NotFound($"Não encontrado categoria com id: {id}");
            }
            return Ok(categoria);
        }
        catch(Exception ex)
        {
            return NotFound(ex.Message);
        }
        
    }

    [HttpPut]
    public ActionResult<Categoria> Update([FromBody]Categoria categoria)
    {
        try
        {
            _repository.Update(categoria);
            return Ok(categoria);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var categoria = _repository.GetById(id);
        if (categoria == null)
        {
            return NotFound($"Categoria com o id: {id} não foi encontrada.");
        }
        _repository.Remove(id);
        return Ok($"A categoria com id: {id} foi deletada com sucesso.");             
        
    }
}
