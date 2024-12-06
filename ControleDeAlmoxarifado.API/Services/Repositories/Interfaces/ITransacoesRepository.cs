namespace ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;

public interface ITransacoesRepository<T> where T : class
{
    IEnumerable<T> GetMonthYear(int mes, int ano);
    IEnumerable<T> GetDate(DateTime date);
    IEnumerable<T> GetByProductName(string nome);
    IEnumerable<T> GetByFuncionario(string nome);
    IEnumerable<T> GetByFornecedor(string nome);
}
