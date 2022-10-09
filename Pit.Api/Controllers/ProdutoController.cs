using Microsoft.AspNetCore.Mvc;
using Pit.Api.Requests;
using Pit.Api.Services.Interfaces;

namespace Pit.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterAtivosAsync()
    {
        var produtos = await _produtoService.ObterProdutosAtivos();

        return Ok(produtos);
    }

    [HttpGet("{filtro}")]
    public async Task<IActionResult> ObterComFiltroAsync(string filtro)
    {
        try
        {
            var resposta = await _produtoService.BuscarProdutoAsync(filtro);
            return StatusCode(StatusCodes.Status200OK, resposta);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Adicionar([FromBody] ProdutoCreateRequest request)
    {
        try
        {
            var resposta = _produtoService.AdicionarProduto(request);
            return StatusCode(StatusCodes.Status201Created, resposta);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarAsync([FromBody] ProdutoDto produtoDto, int id)
    {
        try
        {
            var resposta = await _produtoService.AtualizarProdutoAsync(produtoDto, id);
            return StatusCode(StatusCodes.Status200OK, resposta);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var resposta = _produtoService.DeletarProduto(id);
            return StatusCode(StatusCodes.Status200OK, resposta);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
    }
}
