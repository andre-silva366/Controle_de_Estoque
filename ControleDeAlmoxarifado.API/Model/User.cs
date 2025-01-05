namespace ControleDeAlmoxarifado.API.Model;

public class User
{   
    public int UserId { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string UserRole { get; set; }    
    public string Password { get; set; }

}
