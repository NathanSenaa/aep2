using DoaFacil.Core.Communication;
using DoaFacil.Core.Messages.IntegrationEvents;
using DoaFacil.Estoque.Domain.Interfaces.Infra;
using MediatR;

namespace DoaFacil.Estoque.Domain.Event
{
    public class ProdutoEventHandler :
        INotificationHandler<PedidoIniciadoEvent>,
        INotificationHandler<PedidoReprovadoEvent>
    {

        private readonly IMediatorHandler _mediatorHandler;
        private readonly IEstoqueService _estoqueService;

        public ProdutoEventHandler(IEstoqueService estoqueService, IMediatorHandler mediatorHandler)
        {
            _estoqueService = estoqueService;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(PedidoIniciadoEvent message, CancellationToken cancellationToken)
        {
            var result = await _estoqueService.DebitarListaProdutosPedido(message.ProdutosPedido);

            if (result)
            {
                await _mediatorHandler.PublicarEvento(new PedidoAprovadoEvent(message.PedidoId));
            }else
            {
                await _mediatorHandler.PublicarEvento(new PedidoRejeitadoEvent(message.PedidoId));
            }
        }

        public async Task Handle(PedidoReprovadoEvent message, CancellationToken cancellationToken)
        {
            var result = await _estoqueService.CreditarListaProdutosPedido(message.ProdutosPedido);

            if (!result)
            {
                await _mediatorHandler.PublicarEvento(new ReposicaoProdutosFalhouEvent(message.PedidoId));
            }
        }
    }
}
