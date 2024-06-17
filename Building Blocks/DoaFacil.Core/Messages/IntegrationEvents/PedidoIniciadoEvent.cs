using DoaFacil.Core.DomainObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoaFacil.Core.Messages.IntegrationEvents
{
    public class PedidoIniciadoEvent : IntegrationEvent
    {
        public PedidoIniciadoEvent(Guid pedidoId, Guid solicitanteId, ListaProdutosPedido produtosPedido)
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
