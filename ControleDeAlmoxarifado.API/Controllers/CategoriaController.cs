using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Implements;
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
}
