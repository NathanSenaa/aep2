using DoaFacil.Core.Data;
using DoaFacil.Pedidos.Domain.Models;

namespace DoaFacil.Pedidos.Domain.Interfaces.Infra
{
    public interface IPedidoDoacaoRepository : IRepository<PedidoDoacao>
    {
        Task<PedidoDoacao> ObterPedidoPorId(Guid pedidoId);
        Task AdicionarItem(ItemsPedido item);
        Task AtualizarItem(ItemsPedido item);
    }
}
