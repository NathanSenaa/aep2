using DoaFacil.Estoque.Application.Commands.DTO;
using DoaFacil.Estoque.Application.Commands.Produtos;
using DoaFacil.Estoque.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DoaFacil.API.Controllers
{
    [Route("produto")]
    public class ProdutoController: MainController
    {
        private readonly IMediator _mediatorHandler;
        private readonly IProdutoQueries _pedidoQuere;
        public ProdutoController(IMediator mediatorHandler, IProdutoQueries pedidoQuere)
        {
            _mediatorHandler = mediatorHandler;
            _pedidoQuere = pedidoQuere;
        }

        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarProduto(ProdutoDTO produtoDTO)
        {
            try
            {
                var produto = new AdicionarProdutoCommand(produtoDTO.Nome, produtoDTO.Categoria, produtoDTO.Quantidade, produtoDTO.Codigo);
                return CustomResponse(await _mediatorHandler.Send(produto));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("AdicionarQuantidadeItem")]
        public async Task<IActionResult> AdicionarQuantidadeProduto(AdicionarQuantidadeProdutoDTO produtoItemDTO)
        {
            try
            {
                var produto = new AdicionarItemProdutoCommand(produtoItemDTO.IdProduto, produtoItemDTO.Quantidade);
                return CustomResponse(await _mediatorHandler.Send(produto));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodosProdutos()
        {
            try
            {               
                return CustomResponse(await _pedidoQuere.ObterTodosProdutos());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("ObterPorId{id:guid}")]
        public async Task<IActionResult> ObterProdutoPorId(Guid id)
        {
            try
            {
                return CustomResponse(await _pedidoQuere.ObterProdutosPorId(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
