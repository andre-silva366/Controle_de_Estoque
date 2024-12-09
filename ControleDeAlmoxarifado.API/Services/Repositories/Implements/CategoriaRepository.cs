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
            throw new Exception($"{ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }

    public IEnumerable<Categoria> GetAll()
    {
        try
        {
            _connection.Open ();
            var query = "SELECT Id, Nome FROM Categoria;";
            var categorias = _connection.Query<Categoria>(query).ToList();
            return categorias;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }

    public Categoria GetById(int id)
    {
        try
        {
            _connection.Open();
            var query = "SELECT Id, Nome FROM Categoria WHERE Id = @Id;";
            var categoria = _connection.QuerySingle<Categoria>(query, new {Id = id});
            return categoria;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
        finally
        {
            _connection.Close();
        }
    }

    public void Remove(int id)
    {
        try
        {
            _connection.Open();
            var query = "DELETE FROM Categoria WHERE Id = @Id;";
            _connection.Query(query, new {Id = id});
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            _connection.Close();
        }
    }
}
