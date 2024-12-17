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
            var query = @"INSERT INTO Categoria(Nome) VALUES (@Nome);SELECT LAST_INSERT_ID();";
            var select = @"SELECT Id, Nome FROM Categoria WHERE Id = @Id;";
            var categoriaId = _connection.QuerySingleOrDefault<int>(query, new { categoria.Nome }) ;
            var categoriaAdicionada = _connection.QuerySingleOrDefault<Categoria>(select, new { Id = categoriaId });
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
            return null;
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
            if(_connection.Execute(query, new { Id = id }) == 0)
            {
                throw new Exception($"Não encontrado categoria de id: {id}");
            }
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

    public Categoria Update(Categoria categoria)
    {
        try
        {
            _connection.Open();
            var categoriaAtual = _connection.QuerySingleOrDefault<Categoria>("SELECT * FROM Categoria WHERE Id = @Id;",new {categoria.Id});
            if(categoriaAtual == null)
            {
                throw new Exception($"Não encontrado nenhuma categoria com Id: {categoria.Id}");
            }
            if(_connection.Execute("UPDATE Categoria SET Nome = @Nome WHERE Id = @Id", new {categoria.Nome,categoria.Id}) != 1)
            {
                throw new Exception("Ocorreu um erro ao tentar atualizar.");
            }
            return _connection.QuerySingleOrDefault<Categoria>("SELECT * FROM Categoria WHERE Id = @Id;", new { categoria.Id }) ?? throw new Exception("Ocorreu um erro ao atualizar categoria.");
        }
        finally
        {
            _connection.Close();
        }
    }
}
