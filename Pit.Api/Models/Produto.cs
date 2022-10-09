namespace Pit.Api.Models;

public class Produto : EntidadeBase
{
    public string Nome { get; set; }
    public string CodigoBarras { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public bool Ativo { get; set; }
}
