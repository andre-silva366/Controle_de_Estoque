using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace ControleDeAlmoxarifado.API.Services.Repositories.Implements;

public class FuncionarioRepository : IRepository<Funcionario>
{
    private readonly IDbConnection _connection;
    public FuncionarioRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public Funcionario Add(Funcionario funcionario)
    {
        try
        {
            _connection.Open();
            var queryInsert = @"INSERT INTO Funcionario (Matricula, Nome, Cargo) VALUES (@Matricula, @Nome, @Cargo);SELECT LAST_INSERT_ID();";
            var querySelect = "SELECT Id, Matricula, Nome, Cargo FROM Funcionario WHERE Id = @Id;";
            var funcionarioAdicionadoId = _connection.QuerySingleOrDefault<int>(queryInsert, new { funcionario.Matricula,funcionario.Nome, funcionario.Cargo });
            var funcionarioAdicionado = _connection.QuerySingleOrDefault<Funcionario>(querySelect, new {Id = funcionarioAdicionadoId}) ?? throw new Exception("Erro ao adicionar funcionario");
            return funcionarioAdicionado;
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

    public IEnumerable<Funcionario> GetAll()
    {
        try
        {
            _connection.Open();
            var query = @"SELECT Id, Matricula, Nome, Cargo FROM Funcionario;";
            var funcionarios = _connection.Query<Funcionario>(query).ToList();
            return funcionarios;
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

    public Funcionario GetById(int id)
    {
        try
        {
            _connection.Open();
            var query = "SELECT Id, Matricula, Nome, Cargo FROM Funcionario WHERE Id = @Id;";
            return  _connection.QuerySingle<Funcionario>(query,new {Id = id});
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
            var query = "DELETE FROM Funcionario WHERE Id = @Id;";
            var row = _connection.Execute(query, new {Id = id});
            if(row == 0)
            {
                throw new Exception($"Não encontrado funcionario com o id: {id}");
            }
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

    public Funcionario Update(Funcionario funcionario)
    {
        try
        {
            _connection.Open();
            var querySelect = "SELECT Id, Matricula, Nome, Cargo FROM Funcionario WHERE Id = @Id;";
            var queryUpdate = "UPDATE Funcionario SET Matricula = @Matricula, Nome = @Nome, Cargo = @Cargo WHERE Id = @Id;";
            var funcionarioAtual = _connection.QuerySingleOrDefault<Funcionario>(querySelect,new {funcionario.Id}) ?? throw new Exception($"Não encontrado funcionario com id: {funcionario.Id}");

            if (_connection.Execute(queryUpdate, new { funcionario.Matricula, funcionario.Nome, funcionario.Cargo, funcionario.Id }) != 1)
            {
                throw new Exception("Ocorreu um erro ao tentar atualizar.");
            }

            return _connection.QuerySingle<Funcionario>(querySelect, new { funcionario.Id });
        }
        finally
        {
            _connection.Close();
        }
    }
}
