using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace ControleDeAlmoxarifado.API.Services.Repositories.Implements;

public class CategoriaRepository : IRepository<Categoria>
{
    private readonly IDbConnection _connection;

    public CategoriaRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public Categoria Add(Categoria categoria)
    {
        try
        {
            _connection.Open();
            var query = @"INSERT INTO Categoria(Nome) VALUES (@Nome);";
            var categoriaAdicionada =  _connection.QuerySingle<Categoria>(query, new { categoria.Nome }) ;
            return categoriaAdicionada;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            _connection.Close();
        }
    }

    public IEnumerable<Categoria> GetAll()
    {
        throw new NotImplementedException();
    }

    public Categoria GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Remove(Categoria entity)
    {
        throw new NotImplementedException();
    }
}
