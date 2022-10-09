using Pit.Api.Models;
using Pit.Api.Requests;

namespace Pit.Api.Services.Interfaces;

public interface IProdutoService
{
    Produto AdicionarProduto(ProdutoCreateRequest produto);
    bool DeletarProduto(int? id);
    Task<List<Produto>> ObterProdutosAtivos();
    Task<List<Produto>> BuscarProdutoAsync(string filtro);
    Task<Produto> AtualizarProdutoAsync(ProdutoDto produtoDto, int id);
}
