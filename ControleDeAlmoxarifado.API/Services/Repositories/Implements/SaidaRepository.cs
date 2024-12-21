using ControleDeAlmoxarifado.API.Model;
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
            var queryInsert = @"INSERT INTO Saida (DataSaida, ProdutoId, SolicitanteId, AlmoxarifeId, Quantidade) VALUES (@DataSaida, @ProdutoId, @SolicitanteId, @AlmoxarifeId, @Quantidade); SELECT LAST_INSERT_ID();";

            var querySelect = "SELECT Id, DataSaida, ProdutoId, SolicitanteId, AlmoxarifeId, Quantidade FROM Saida WHERE Id = @Id;";

            var parameters = new
            {
                saida.DataSaida,
                saida.ProdutoId,
                saida.SolicitanteId,
                saida.AlmoxarifeId,
                saida.Quantidade
            };

            var saidaAdicionadaId = _connection.QuerySingleOrDefault<int>(queryInsert, parameters);
            if(saidaAdicionadaId == 0)
            {
                throw new Exception("Ocorreu um erro ao inserir a saida.");
            }

            var queryQuantidade = "SELECT Quantidade FROM Produto WHERE Id = @Id";
            var quantidadeProduto = _connection.QuerySingleOrDefault<int>(queryQuantidade, new {Id = saida.ProdutoId});

            if (saida.Quantidade > quantidadeProduto)
            {
                throw new Exception("Não é possível retirar essa quantidade de produto.");
            }

            var subtraindo = "UPDATE Produto SET Quantidade = Quantidade - @Quantidade WHERE Id = @Id;";
            _connection.Execute(subtraindo, new {saida.Quantidade, Id = saida.ProdutoId});

            var saidaAdicionada = _connection.QuerySingleOrDefault<Saida>(querySelect, new { Id = saidaAdicionadaId }) ?? throw new Exception("Ocorreu um erro ao retornar a saida adicionada");
            return saidaAdicionada;
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

    public void Remove(int id)
    {
        try
        {
            var querySelect = "SELECT * FROM Saida WHERE Id = @Id;";
            var queryDelete = "DELETE FROM Saida WHERE Id = @Id;";

            var saida = _connection.QuerySingleOrDefault<Saida>(querySelect, new { Id = id });
            if(saida == null)
            {
                throw new Exception($"Não encontrada saida com id: {id}");
            }
            _connection.Execute(queryDelete, new {saida.Id});
        }
        finally
        {
            _connection.Close();
        }
    }

}
