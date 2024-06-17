using DoaFacil.Core.Communication;
using DoaFacil.Core.Messages.IntegrationEvents;
using DoaFacil.Pedidos.Application.Commands.Pedidos;
using DoaFacil.Pedidos.Domain.Interfaces.Infra;
using MediatR;

namespace DoaFacil.Pedidos.Application.Event
{
    public class PedidoEventHanlder :
        INotificationHandler<PedidoAprovadoEvent>,
        INotificationHandler<PedidoRejeitadoEvent>,
        INotificationHandler<ReposicaoProdutosFalhouEvent>
    {
        private readonly IPedidoDoacaoRepository _pedidoDoacaoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PedidoEventHanlder(IPedidoDoacaoRepository pedidoDoacaoRepository, IMediatorHandler mediatorHandler)
        {
            _pedidoDoacaoRepository = pedidoDoacaoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(PedidoAprovadoEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new PedidoAprovadoCommand(message.PedidoId));
        }
        public async Task Handle(PedidoRejeitadoEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new PedidoReprovadoCommand(message.PedidoId));
        }

        public async Task Handle(ReposicaoProdutosFalhouEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new ReposicaoProdutoFalhouCommand(message.PedidoId));
        }
    }
}
