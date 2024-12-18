using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeAlmoxarifado.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly IRepository<Produto> _repository;

    public ProdutoController(IRepository<Produto> repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public IActionResult Create([FromBody]Produto produto)
    {
        if(produto == null)
        {
            return BadRequest();
        }
        
        return Ok(_repository.Add(produto));
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetAll()
    {
        try
        {
            var produtos = _repository.GetAll().ToList();            
            return Ok(produtos);
        }
        catch (Exception ex)
        {
            return NotFound($"{ex.Message}");
        }
        
    }

    [HttpGet("{id:int}")]
    public ActionResult<Produto> GetById(int id)
    {
        var produto = _repository.GetById(id);
        if (produto == null)
        {
            return NotFound($"Não encontrado produto com id: {id}");
        }
        return Ok(produto);

    }

    [HttpPut]
    public ActionResult<Produto> Update([FromBody]Produto produto)
    {
        try
        {
            return Ok(_repository.Update(produto));
        }
        catch(Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var produto = _repository.GetById(id);
        if (produto == null)
        {
            return NotFound($"O produto com id: {id} não foi encontrado.");
        }
        _repository.Remove(id);
        return Ok($"O produto com id: {id} foi deletado com sucesso.");
    }
}
