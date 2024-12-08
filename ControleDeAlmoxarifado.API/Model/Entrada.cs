using ControleDeAlmoxarifado.API.Services;
using System.Text.Json.Serialization;

namespace ControleDeAlmoxarifado.API.Model;

public class Entrada
{
    public int Id { get; set; }
    public DateTime DataEntrada { get; set; } = DateTime.Now;
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal PreçoTotal => PrecoUnitario * Quantidade;
    public int FornecedorId { get; set; }
    public int FuncionarioId { get; set; }
}
