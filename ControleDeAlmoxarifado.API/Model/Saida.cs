namespace ControleDeAlmoxarifado.API.Model;

public class Saida
{
    public int Id { get; set; }
    public DateTime DataSaida { get; set; }
    public int IdProduto { get; set; }
    public int IdSolicitante { get; set; }
    public int IdAlmoxarife { get; set; }
    public int Quantidade { get; set; }
}
