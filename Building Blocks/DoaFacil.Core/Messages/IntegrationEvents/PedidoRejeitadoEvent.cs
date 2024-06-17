using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoaFacil.Core.Messages.IntegrationEvents
{
    public class PedidoRejeitadoEvent : IntegrationEvent
    {
        public PedidoRejeitadoEvent(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }

        public Guid PedidoId { get; private set; }
    }
}
