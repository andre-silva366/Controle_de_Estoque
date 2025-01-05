using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeAlmoxarifado.API.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository<User> _repository;

    public UserController(IUserRepository<User> repository)
    {
        _repository = repository;
    }

    [Route("api/[Controller]/Create")]
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

    [Route("api/[Controller]/Authenticate")]
    [HttpGet]
    public ActionResult<User> Authenticate(string usuario, string senha)
    {
        try
        {
            return Ok(_repository.Authenticate(usuario,senha));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
