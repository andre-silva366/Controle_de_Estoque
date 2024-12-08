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
}
