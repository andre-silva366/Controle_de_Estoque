﻿using ControleDeAlmoxarifado.API.Model;
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
            var query = @"INSERT INTO Funcionario (Matricula, Nome, Cargo) VALUES (@Matricula, @Nome, @Cargo);";
            var funcionarioAdicionado = _connection.QuerySingle<Funcionario>(query, new { funcionario.Matricula,funcionario.Nome, funcionario.Cargo });
            return funcionarioAdicionado;
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
            return null;
        }
        finally
        {
            _connection.Close();
        }
    }

    public Funcionario GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }
}
