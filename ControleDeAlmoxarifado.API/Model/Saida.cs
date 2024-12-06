namespace ControleDeAlmoxarifado.API.Model;

public class Saida
{
    public int Id { get; set; }
    public DateTime DataSaida { get; set; }
    public int ProdutoId { get; set; }
    public int SolicitanteId { get; set; }
    public int AlmoxarifeId { get; set; }
    public int Quantidade { get; set; }
}
