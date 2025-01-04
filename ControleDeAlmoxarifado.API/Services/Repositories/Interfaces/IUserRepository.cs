using ControleDeAlmoxarifado.API.Model;

namespace ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;

public interface IUserRepository<T> where T : class
{
    T CreateUser(T user);
    ICollection<T> GetUsers();
    void Remove(int id);
    T Update(T user);
    T Authenticate(string nome, string senha);
    bool VerifyPassword(string password, string storeHash);
}
