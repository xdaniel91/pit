using Microsoft.EntityFrameworkCore;
using Pit.Api.Models;
using Pit.Api.Requests;
using Pit.Api.Services.Interfaces;
using Pit.Database;

namespace Pit.Api.Services;

public class ProdutoService : IProdutoService
{
    private readonly PitContext _context;

    public ProdutoService(PitContext context)
    {
        _context = context;
    }

    public async Task<Produto> AtualizarProdutoAsync(ProdutoDto produtoDto)
    {
        var produtoAtualizar = await ObterProdutoPeloIdAsync(produtoDto.Id);

        if (!string.IsNullOrEmpty(produtoDto.Nome))
            produtoAtualizar.Nome = produtoDto.Nome;

        if (!string.IsNullOrEmpty(produtoDto.Descricao))
            produtoAtualizar.Descricao = produtoDto.Descricao;

        if (produtoDto.Valor > 0)
            produtoAtualizar.Valor = produtoDto.Valor;

        if (string.IsNullOrEmpty(produtoDto.Codigo))
            produtoAtualizar.CodigoBarras = produtoDto.Codigo;

        var entry = _context.produto.Update(produtoAtualizar);

        return entry.Entity;
    }

    public Produto AdicionarProduto(Produto produto)
    {
        if (string.IsNullOrEmpty(produto.Nome))
            throw new Exception("Informe o nome do produto.");

        if (string.IsNullOrEmpty(produto.CodigoBarras))
            throw new Exception("Informe o código de barras do produto.");

        if (string.IsNullOrEmpty(produto.CodigoBarras))
            throw new Exception("Informe a descrição do produto.");

        if (produto.Valor <= 0)
            throw new Exception("Informe um valor válido para o produto.");

        produto.Nome = produto.Nome.ToLower();
        produto.CodigoBarras = produto.CodigoBarras.ToLower();
        produto.Descricao = produto.Descricao.ToLower();
        produto.DataHoraCriado = DateTime.UtcNow;
        produto.Ativo = true;


        try
        {
            var entry = _context.produto.Add(produto);
            var resultado = _context.SaveChanges();

            if (resultado > 0)
                return entry.Entity;
            else
                throw new Exception("Erro ao inserir produto no banco de dados.");
        }
        catch (Exception)
        {
            throw;
        }
    }

    public bool DeletarProduto(int? id)
    {
        if (id <= 0)
            throw new Exception("Id inválido.");

        var produto = _context.produto.FirstOrDefault(x => x.Id == id);

        if (produto is null)
            throw new Exception("Produto não encontrado.");

        else
        {
            produto.Ativo = false;
            _context.Update(produto);
            var resultado = _context.SaveChanges();
            if (resultado > 0)
                return true;
            else
                return false;
        }
    }

    public async Task<List<Produto>> ObterProdutosAtivos()
    {
        return await _context.produto.Where(x => x.Ativo).OrderBy(x => x.Id).ToListAsync();
    }

    public async Task<List<Produto>> BuscarProdutoAsync(string filtro)
    {
        if (string.IsNullOrEmpty(filtro))
            return await ObterProdutosAtivos();
        else
        {
            filtro = filtro.ToLower();
            var listaFiltrada = await _context.produto.Where(x => x.Nome.Contains(filtro) || x.CodigoBarras.Contains(filtro)).ToListAsync();

            if (listaFiltrada.Count <= 0)
                return await ObterProdutosAtivos();
            else
                return listaFiltrada;
        }
    }

    private async Task<Produto> ObterProdutoPeloIdAsync(int id)
    {
        return await _context.produto.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
}
