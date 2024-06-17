using DoaFacil.Pedidos.Domain.Interfaces.Infra;
using DoaFacil.Pedidos.Domain.Models;

namespace DoaFacil.Pedidos.Infra.Data.Data.Repository
{
    public class PedidoDoacaoRepository : Repository<PedidoDoacao>, IPedidoDoacaoRepository
    {
        public PedidoDoacaoRepository(PedidosContext db) : base(db)
        {
        }

        public async Task AdicionarItem(ItemsPedido item)
        {
            await Db.ItemsPedido.AddAsync(item);
        }

        public async Task AtualizarItem(ItemsPedido item)
        {
            await Task.Run(() =>
            {
                Db.ItemsPedido.Update(item);
            });
        }

        public async Task<PedidoDoacao> ObterPedidoPorId(Guid pedidoId)
        {
            var pedido = await DbSet.FindAsync(pedidoId);
            if (pedido == null) return null;

            await DbSet.Entry(pedido)
                .Collection(i => i.PedidoItems).LoadAsync();

            return pedido;
        }
    }
}
