using Dapper;
using System.Data;
namespace ControleDeAlmoxarifado.API.Model;

public class User
{   
    public int UserId { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; } = "xyz";
    public string Role { get; set; }    
    public string Password { get; set; }

}
