using DoaFacil.Core.DomainObjects.DTO;

namespace DoaFacil.Core.Messages.IntegrationEvents
{
    public class PedidoReprovadoEvent : IntegrationEvent
    {
        public PedidoReprovadoEvent(Guid pedidoId, Guid solicitanteId, ListaProdutosPedido produtosPedido)
        {
            PedidoId = pedidoId;
            SolicitanteId = solicitanteId;
            ProdutosPedido = produtosPedido;
        }

        public Guid PedidoId { get; private set; }
        public Guid SolicitanteId { get; private set; }
        public ListaProdutosPedido ProdutosPedido { get; private set; }
    }
}
