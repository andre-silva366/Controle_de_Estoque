namespace ControleDeAlmoxarifado.API.Model;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int Quantidade { get; set; }
    public int CategoriaId { get; set; }
    public int FornecedorId { get; set; }
}
