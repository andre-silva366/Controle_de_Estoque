using ControleDeAlmoxarifado.API.Model;
using System.Data;
using Dapper;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using System.Data.Common;

namespace ControleDeAlmoxarifado.API.Services.Repositories.Implements;

public class UserRepository : IUserRepository<User>
{
    private readonly IDbConnection _connection;
    public UserRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public User CreateUser(User user)
    {
        try
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var queryInsert = "INSERT INTO Users (Username, PasswordHash, UserRole) VALUES (@Username, @PasswordHash, @UserRole);SELECT LAST_INSERT_ID()";

            var querySelect = "SELECT UserId, Username, UserRole FROM Users WHERE UserId = @Id;";

            var userId = _connection.QuerySingleOrDefault<int>(queryInsert, new { user.Username, PasswordHash = passwordHash, user.UserRole });

            var userCriado = _connection.QuerySingleOrDefault<User>(querySelect, new { Id = userId });

            if (userCriado == null)
            {
                throw new Exception("Ocorreu um erro, usuario não criado");
            }
            return userCriado;
        }
        finally
        {
            _connection.Close();
        }
    }
    public ICollection<User> GetUsers()
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public User Update(User entity)
    {
        throw new NotImplementedException();
    }
        
    public User Authenticate(string username, string password)
    {
        try
        {
            var user = _connection.QuerySingleOrDefault<User>("SELECT * FROM Users WHERE Username = @Username", new { Username = username });

            if (user == null)
            {
                throw new Exception("Usuario não encontrado!");
            }
            if (!VerifyPassword(password, user.PasswordHash))
            {
                throw new Exception("Dados inválidos, verifique o usuario e a senha!");
            }

            return new User
            {
                UserId = user.UserId,
                Username = username,
                UserRole = user.UserRole
            };
        }
        finally 
        { 
            _connection.Close(); 
        }
        
    }
    public bool VerifyPassword(string password, string storeHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storeHash);
    }

    
}
