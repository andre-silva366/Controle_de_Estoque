namespace ControleDeAlmoxarifado.API.Model;

public class Entrada
{
    public int Id { get; set; }
    public DateTime DataEntrada { get; set; }
    public int IdProduto { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public int PreçoTotal { get; set; }
    public int IdFornecedor { get; set; }
    public Produto Produto { get; set; }
    public Fornecedor Fornecedor { get; set; }
    public Funcionario Funcionario { get; set; }
}
