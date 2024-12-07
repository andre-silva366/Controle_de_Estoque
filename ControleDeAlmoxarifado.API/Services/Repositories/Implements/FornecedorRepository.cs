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
            var fornecedorAdicionado = _connection.QuerySingle<Fornecedor>(query, new {fornecedor.Nome, fornecedor.Telefone, fornecedor.Email, fornecedor.Cnpj});
            return fornecedorAdicionado;
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

    public IEnumerable<Fornecedor> GetAll()
    {
        throw new NotImplementedException();
    }

    public Fornecedor GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Remove(Fornecedor entity)
    {
        throw new NotImplementedException();
    }
}
