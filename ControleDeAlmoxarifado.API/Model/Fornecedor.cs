namespace ControleDeAlmoxarifado.API.Model;

public class Fornecedor
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string CpfCnpj { get; set; }
    public ICollection<Produto> Produtos { get; set; }
}
