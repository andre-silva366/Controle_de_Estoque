using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Dapper;
using System.Data;
using System.Reflection;

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

            if (_connection.Execute(query, parameters) != 1)
            {
                throw new Exception("Ocorreu um erro ao adicionar a saida!");
            }

            var saidaAdicionada = _connection.QuerySingleOrDefault<Saida>("SELECT * FROM Saida WHERE Id = @Id", new { saida.Id });
            return saidaAdicionada;
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

    public IEnumerable<Saida> GetAll()
    {
        try
        {
            _connection.Open();
            var query = @"SELECT Id, DataSaida, ProdutoId, SolicitanteId, AlmoxarifeId, Quantidade FROM Saida;";

            var saidas = _connection.Query<Saida>(query).ToList();
            if(saidas.Count == 0)
            {
                throw new Exception("Não existem dados de saida cadastrados.");
            }
            return saidas;
        }        
        finally
        {
            _connection.Close();
        }
    }

    public IEnumerable<Saida> GetByCode(string codigo)
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

    public IEnumerable<Saida> GetByDate(DateTime date)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Saida> GetByMonthYear(int mes, int ano)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public Saida Update(Saida saida)
    {
        try
        {
            var querySelect = "SELECT * FROM Saida WHERE Id = @Id";
            var queryUpdate = "UPDATE Saida SET DataSaida = @DataSaida, ProdutoId = @ProdutoId, SolicitanteId = @SolicitanteId, AlmoxarifeId = @AlmoxarifeId, Quantidade = @Quantidade WHERE Id = @Id";
            var saidaAtual = _connection.QuerySingle<Saida>(querySelect, new {saida.Id});
            if(saidaAtual == null)
            {
                throw new Exception($"Não encontrado nenhuma saida com id: {saida.Id}");
            }
            if(_connection.Execute(queryUpdate, new {saida.DataSaida, saida.ProdutoId, saida.SolicitanteId, saida.AlmoxarifeId, saida.Quantidade, saida.Id}) != 1)
            {
                throw new Exception("Ocorreu um erro ao atualizar a saida.");
            };
            var saidaAtualizada = _connection.QuerySingle<Saida>(querySelect,new {saida.Id});
            return saidaAtualizada;
        }
        finally
        {
            _connection.Close();
        }
    }
}
