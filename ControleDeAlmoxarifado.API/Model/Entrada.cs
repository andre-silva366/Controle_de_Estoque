namespace ControleDeAlmoxarifado.API.Model;

public class Entrada
{
    public int Id { get; set; }
    public DateTime DataEntrada { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public int PreçoTotal { get; set; }
    public int FornecedorId { get; set; }
    public int FuncionarioId { get; set; }
}
