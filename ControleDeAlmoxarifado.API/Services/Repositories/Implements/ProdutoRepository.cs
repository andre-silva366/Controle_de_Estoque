using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace ControleDeAlmoxarifado.API.Services.Repositories.Implements;

public class ProdutoRepository : IRepository<Produto>
{
    private readonly IDbConnection _connection;
    public ProdutoRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public Produto Add(Produto produto)
    {
        try
        {
            _connection.Open();
            var query = @"INSERT INTO Produto (Nome, Descricao, Quantidade, CategoriaId, FornecedorId,Codigo) VALUES (@Nome, @Descricao, @Quantidade, @CategoriaId, @FornecedorId);";
            var produtoAdicionado = _connection.QuerySingle<Produto>(query, new {produto.Nome,produto.Descricao,produto.Quantidade,produto.CategoriaId,produto.FornecedorId,produto.Codigo});
            return produtoAdicionado;
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

    public IEnumerable<Produto> GetAll()
    {
        throw new NotImplementedException();
    }

    public Produto GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Remove(Produto entity)
    {
        throw new NotImplementedException();
    }
}

