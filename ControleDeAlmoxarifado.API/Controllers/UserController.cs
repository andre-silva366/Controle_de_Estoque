using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeAlmoxarifado.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository<User> _repository;

    public UserController(IUserRepository<User> repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public ActionResult<User> Create (User user)
    {
        try
        {
            return Ok( _repository.CreateUser(user));
        }
        catch (Exception ex)
        {
            return BadRequest( ex.Message );
        }
    }
}
