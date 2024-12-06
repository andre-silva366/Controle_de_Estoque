using Microsoft.AspNetCore.Mvc;

namespace ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T Add(T entity);
    void Remove(T entity);
    T GetById(int id);

}
