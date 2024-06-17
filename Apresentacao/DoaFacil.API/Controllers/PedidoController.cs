using DoaFacil.Estoque.Application.Queries;
using DoaFacil.Pedidos.Application.Commands.DTO;
using DoaFacil.Pedidos.Application.Commands.Pedidos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DoaFacil.API.Controllers
{
    [Route("pedido")]
    public class PedidoController : MainController
    {
        private readonly IMediator _mediatorHandler;
        private readonly IProdutoQueries _produtoQuere;        
        public PedidoController(IMediator mediatorHandler, IProdutoQueries pedidoQuere)
        {
            _mediatorHandler = mediatorHandler;
            _produtoQuere = pedidoQuere;
        }

        [HttpPost("CriarPedido")]
        public async Task<IActionResult> CriarPedido(PedidoDTO pedidoDto)
        {
            try
            {
                var pedido = new PedidoCommand(pedidoDto.IdSolicitante);
                return CustomResponse(await _mediatorHandler.Send(pedido));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("AdicionarItemPedido")]
        public async Task<IActionResult> AdicionarItem(ItemPedidoDTO itemPedido)
        {
            try
            {
                var produto = await _produtoQuere.ObterProdutosPorId(itemPedido.IdProduto);
                if (produto == null){
                    AdicionarErroProcessamento("Produto não localizado");
                    return CustomResponse();
                }
                var pedido = new AdicionarItemPedidoCommand(itemPedido.IdPedido, itemPedido.IdProduto, itemPedido.Quantidade, produto.Nome);
                return CustomResponse(await _mediatorHandler.Send(pedido));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("emitir-pedido/{pedidoId:guid}")]
        public async Task<IActionResult> EmitirPedido(Guid pedidoId)
        {
            try
            {
                var pedidoEmitidoCommand = new SolicitarPedidoCommand(pedidoId);
                return CustomResponse(await _mediatorHandler.Send(pedidoEmitidoCommand));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("cancelar-pedido/{pedidoId:guid}")]
        public async Task<IActionResult> CancelarPedido(Guid pedidoId)
        {
            try
            {
                var cancelarPedidoCommand = new CancelarPedidoCommand(pedidoId);
                return CustomResponse(await _mediatorHandler.Send(cancelarPedidoCommand));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }        
    }
}
