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
            var queryInsert = @"INSERT INTO Produto (Nome, Descricao, Quantidade, CategoriaId, FornecedorId,Codigo) VALUES (@Nome, @Descricao, @Quantidade, @CategoriaId, @FornecedorId,@Codigo);SELECT LAST_INSERT_ID();";

            var querySelect = "SELECT Id, Nome, Descricao, CategoriaId, FornecedorId, Codigo, Quantidade FROM Produto WHERE Id = @Id";

            var produtoId = _connection.QuerySingleOrDefault<int>(queryInsert, new{produto.Nome,produto.Descricao,produto.Quantidade,produto.CategoriaId,produto.FornecedorId,produto.Codigo});

            return _connection.QuerySingleOrDefault<Produto>(querySelect,new {Id = produtoId});
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

    public IEnumerable<Produto> GetAll()
    {
        try
        {
            _connection.Open();
            var query = @"SELECT Id ,Nome, Descricao, Quantidade, CategoriaId, FornecedorId,Codigo FROM Produto;";
            var produtos = _connection.Query<Produto>(query).ToList();
            if (produtos.Count == 0)
            {
                throw new Exception("Não existem produtos cadastrados.");
            }
            return produtos;
        }
        finally
        {
            _connection.Close();
        }
    }

    public Produto GetById(int id)
    {
        try
        {
            _connection.Open();
            var query = "SELECT Id ,Nome, Descricao, Quantidade, CategoriaId, FornecedorId,Codigo FROM Produto WHERE Id = @Id;";
            var produto = _connection.QuerySingleOrDefault<Produto>(query, new {Id = id});
            return produto;
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
            var query = "DELETE FROM Produto WHERE Id = @Id ;";
            if(_connection.Execute(query, new { Id = id }) == 0)
            {
                throw new Exception($"Não encontrado Produto com id: {id}");
            }
            
        }        
        finally
        {
            _connection.Close();
        }
    }

    public Produto Update(Produto produto)
    {
        try
        {
            _connection.Open();
            var querySelect = "SELECT Id, Nome, Descricao, CategoriaId, FornecedorId, Codigo, Quantidade FROM Produto WHERE Id = @Id;";
            var produtoAtual = _connection.QuerySingleOrDefault<Produto>(querySelect,new {produto.Id}) ?? throw new Exception($"Não encontrado produto com id: {produto.Id}") ;

            if (_connection.Execute("UPDATE Produto SET Nome = @Nome, Descricao = @Descricao, CategoriaId = @CategoriaId, FornecedorId = @FornecedorId, Codigo = @Codigo, Quantidade = @Quantidade WHERE Id = @Id", new { produto.Nome, produto.Descricao, produto.CategoriaId, produto.FornecedorId, produto.Codigo, produto.Quantidade, produto.Id }) != 1)
            {
                throw new Exception("Ocorreu um erro ao tentar atualizar.");
            }
            return _connection.QuerySingle<Produto>(querySelect, new { produto.Id });
        }
        finally
        {
            _connection.Close();
        }
    }
}

