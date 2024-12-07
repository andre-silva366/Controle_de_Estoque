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
}
