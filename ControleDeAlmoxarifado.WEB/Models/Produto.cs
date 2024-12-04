namespace ControleDeAlmoxarifado.WEB.Models;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int Quantidade { get; set; }
    public int CategoriaId { get; set; }
    public int FornecedorId { get; set; }
    public Categoria Categoria { get; set; }
    public Fornecedor Fornecedor { get; set; }
}
