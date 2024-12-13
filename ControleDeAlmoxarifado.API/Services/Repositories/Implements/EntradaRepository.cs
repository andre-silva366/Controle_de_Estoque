using ControleDeAlmoxarifado.API.Model;
using ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;
using Dapper;
using System.Data;

namespace ControleDeAlmoxarifado.API.Services.Repositories.Implements;

public class EntradaRepository : IRepository<Entrada>, ITransacoesRepository<Entrada>
{
    private readonly IDbConnection _connection;

    public EntradaRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public Entrada Add(Entrada entrada)
    {
        try
        {
            _connection.Open();
            var query = @"INSERT INTO Entrada (DataEntrada, ProdutoId, Quantidade, PrecoUnitario, PrecoTotal, FornecedorId, FuncionarioId) VALUES (@DataEntrada, @ProdutoId, @Quantidade, @PrecoUnitario, @PrecoTotal, @FornecedorId, @FuncionarioId);";

            var parameters = new
            {
                entrada.DataEntrada, 
                entrada.ProdutoId, 
                entrada.Quantidade, 
                entrada.PrecoUnitario,
                PrecoTotal = entrada.Quantidade * entrada.PrecoUnitario,
                entrada.FornecedorId,
                entrada.FuncionarioId
            };

            var entradaAdicionada = _connection.QuerySingle<Entrada>(query,parameters);
            return entradaAdicionada;
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

    public IEnumerable<Entrada> GetAll()
    {
        try
        {
            _connection.Open();
            var query = @"SELECT Id,DataEntrada, ProdutoId, Quantidade, PrecoUnitario, PrecoTotal, FornecedorId, FuncionarioId FROM Entrada;";            
            var entradas = _connection.Query<Entrada>(query).ToList();
            if(entradas.Count == 0)
            {
                throw new Exception("Não existem dados cadastrados.");
            }
            return entradas;
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

    public IEnumerable<Entrada> GetByCode(string codigo)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Entrada> GetByFornecedor(string nome)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Entrada> GetByFuncionario(string nome)
    {
        throw new NotImplementedException();
    }

    public Entrada GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Entrada> GetByProductName(string nome)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Entrada> GetByDate(DateTime date)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Entrada> GetByMonthYear(int mes, int ano)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public Entrada Update(Entrada entity)
    {
        throw new NotImplementedException();
    }
}
