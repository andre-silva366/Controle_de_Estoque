﻿using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace ControleDeAlmoxarifado.API.Services.Repositories.Implements;

public class SaidaRepository : IRepository<Saida>, ITransacoesRepository<Saida>
{
    private readonly IDbConnection _connection;

    public SaidaRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public Saida Add(Saida saida)
    {
        try
        {
            _connection.Open();
            var query = @"INSERT INTO Saida (DataSaida, ProdutoId, SolicitanteId, AlmoxarifeId, Quantidade) VALUES (@DataSaida, @ProdutoId, @SolicitanteId, @AlmoxarifeId, @Quantidade);";

            var parameters = new
            {
                saida.DataSaida,
                saida.ProdutoId,
                saida.SolicitanteId,
                saida.AlmoxarifeId,
                saida.Quantidade
            };

            var saidaAdicionada = _connection.QuerySingle<Saida>(query,parameters);
            return saidaAdicionada;
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

    public IEnumerable<Saida> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Saida> GetByFornecedor(string nome)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Saida> GetByFuncionario(string nome)
    {
        throw new NotImplementedException();
    }

    public Saida GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Saida> GetByProductName(string nome)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Saida> GetDate(DateTime date)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Saida> GetMonthYear(int mes, int ano)
    {
        throw new NotImplementedException();
    }

    public void Remove(Saida entity)
    {
        throw new NotImplementedException();
    }
}