﻿using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Dapper;
using System.Data;
using System.Reflection;

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
            var query = @"INSERT INTO Produto (Nome, Descricao, Quantidade, CategoriaId, FornecedorId,Codigo) VALUES (@Nome, @Descricao, @Quantidade, @CategoriaId, @FornecedorId,@Codigo);";

            var select = "SELECT * FROM Produto WHERE Codigo = @Codigo";

            _connection.QuerySingle<Produto>(query, new{produto.Nome,produto.Descricao,produto.Quantidade,produto.CategoriaId,produto.FornecedorId,produto.Codigo});
            Thread.Sleep(300);
            var produtoAdicionado = _connection.QuerySingle<Produto>(select,new {produto.Codigo});
            return produtoAdicionado;
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

    public Produto Update(Produto entity)
    {
        throw new NotImplementedException();
    }
}

