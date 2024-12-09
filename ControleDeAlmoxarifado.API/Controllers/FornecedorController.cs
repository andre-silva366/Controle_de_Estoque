using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeAlmoxarifado.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FornecedorController : ControllerBase
{
    private readonly IRepository<Fornecedor> _repository;

    public FornecedorController(IRepository<Fornecedor> repository)
    {
        _repository = repository;   
    }

    [HttpPost]
    public IActionResult Create(Fornecedor fornecedor)
    {
        if (fornecedor == null)
        {
            return BadRequest();
        }
        return Ok(_repository.Add(fornecedor));
    }

    [HttpGet]
    public ActionResult<IEnumerable<Fornecedor>> GetAll()
    {
        var fornecedores = _repository.GetAll().ToList();
        if(!fornecedores.Any())
        {
            return NoContent();
        }
        return fornecedores;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Fornecedor> GetById(int id)
    {
        var fornecedor = _repository.GetById(id);
        if(fornecedor == null)
        {
            return NotFound($"O fornecedor com id: {id} não foi encontrado.");
        }
        return Ok(fornecedor);
    }

    [HttpDelete]
    public ActionResult Delete(int id)
    {
        try
        {
            var fornecedor = _repository.GetById(id);
            if (fornecedor == null)
            {
                return NotFound($"O fornecedor com id: {id} não foi encontrado.");
            }
            _repository.Remove(id);
            return Ok($"O fornecedor com id: {id} foi deletado com sucesso");
        }
        catch (Exception ex)
        {
            return BadRequest("Ocorreu um erro durante a operação.");
        }
    }
}
