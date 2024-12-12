namespace ControleDeAlmoxarifado.API.Services.Repositories.Interfaces;

public interface ITransacoesRepository<T> where T : class
{
    IEnumerable<T> GetByMonthYear(int mes, int ano);
    IEnumerable<T> GetByDate(DateTime date);
    IEnumerable<T> GetByProductName(string nome);
    IEnumerable<T> GetByFuncionario(string nome);
    IEnumerable<T> GetByFornecedor(string nome);
    IEnumerable<T> GetByCode(string codigo);
}
