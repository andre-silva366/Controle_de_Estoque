using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace ControleDeAlmoxarifado.API.Services.Repositories.Implements;

public class FornecedorRepository : IRepository<Fornecedor>
{
    private readonly IDbConnection _connection;
    public FornecedorRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public Fornecedor Add(Fornecedor fornecedor)
    {
        try
        {
            _connection.Open();
            var query = @"INSERT INTO Fornecedor (Nome, Telefone, Email, Cnpj) VALUES (@Nome, @Telefone, @Email, @Cnpj)";
            var select = "SELECT * FROM Fornecedor WHERE Nome = @Nome;";
            _connection.QuerySingleOrDefault<Fornecedor>(query, new {fornecedor.Nome, fornecedor.Telefone, fornecedor.Email, fornecedor.Cnpj});
            var fornecedorAdicionado = _connection.QuerySingleOrDefault<Fornecedor>(select, new { fornecedor.Nome });

            return fornecedorAdicionado;
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

    public IEnumerable<Fornecedor> GetAll()
    {
        try
        {
            _connection.Open();
            var query = @"SELECT Id, Nome, Telefone, Email, Cnpj FROM Fornecedor";
            var fornecedores = _connection.Query<Fornecedor>(query).ToList();
            return fornecedores;
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

    public Fornecedor GetById(int id)
    {
        try
        {
            _connection.Open();
            var query = "SELECT Id, Nome, Telefone, Email, Cnpj FROM Fornecedor  WHERE Id = @Id";
            var fornecedor = _connection.QuerySingle<Fornecedor>(query,new {Id = id});
            return fornecedor;
        }
        catch(Exception ex)
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
            var query = "DELETE FROM Fornecedor WHERE Id = @Id";
            _connection.Query(query, new {Id = id});
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

    public Fornecedor Update(Fornecedor fornecedor)
    {
        try
        {
            _connection.Open();
            var querySelect = "SELECT Id, Nome, Telefone, Email, Cnpj FROM Fornecedor WHERE Id = @Id;";
            var fornecedorAtual = _connection.QuerySingle<Fornecedor>(querySelect, new {fornecedor.Id}) ?? throw new Exception($"Não encontrado fornecedor com id: {fornecedor.Id}");
            if(_connection.Execute("UPDATE Fornecedor SET Nome = @Nome, Telefone = @Telefone, Email = @Email, Cnpj = @Cnpj WHERE Id = @Id", new {fornecedor.Nome, fornecedor.Telefone, fornecedor.Email, fornecedor.Cnpj, fornecedor.Id}) != 1)
            {
                throw new Exception("Ocorreu um erro ao atualizar");
            }
            return _connection.QuerySingle<Fornecedor>(querySelect, new {fornecedor.Id});
        }
        finally
        {
            _connection.Close();
        }
    }
}
