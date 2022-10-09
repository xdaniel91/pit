namespace Pit.Api.Requests;

public class ProdutoCreateRequest
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string CodigoBarras { get; set; }
    public decimal Valor { get; set; }

}
