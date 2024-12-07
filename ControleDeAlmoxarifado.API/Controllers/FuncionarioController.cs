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
}
