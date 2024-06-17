using DoaFacil.Core.DomainObjects;

namespace DoaFacil.Pedidos.Domain.Models
{
    public class PedidoDoacao : Entity
    {
        public PedidoDoacao()
        {
            _pedidoItems = new List<ItemsPedido>();
        }
        public PedidoDoacao(Guid idSolicitante)
        {
            IdSolicitante = idSolicitante;
            DataSolicitacao = DateTime.Now;
            _pedidoItems = new List<ItemsPedido>();
        }

        public int Codigo { get; private set; }
        public DateTime DataSolicitacao { get; private set; }
        public Guid IdSolicitante { get; private set; }
        public int QuantidadeTotal { get; private set; }
        public StatusPedido StatusPedido { get; private set; }
        public Solicitante Solicitante { get; private set; }

        private readonly List<ItemsPedido> _pedidoItems;
        public IReadOnlyCollection<ItemsPedido> PedidoItems => _pedidoItems;

        public void AdicionarItemPedido(ItemsPedido itemPedido)
        {
            if (PedidoItemExistente(itemPedido))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(i => i.IdProduto == itemPedido.IdProduto);
                itemExistente.AtualizarQuantidade(itemPedido.Quantidade);
                itemPedido = itemExistente;
                _pedidoItems.Remove(itemExistente);
            }
            _pedidoItems.Add(itemPedido);
            CalcularQuantidadePedido();
        }

        public void RemoverItemPedido(ItemsPedido item)
        {
            if (PedidoItemExistente(item))
            {
                _pedidoItems.Remove(item);
                CalcularQuantidadePedido();
            }
            else
            {
                throw new DomainException("Item não localizado no pedido!");
            }

        }

        public bool PedidoItemExistente(ItemsPedido items)
        {
            return _pedidoItems.Any(i => i.IdProduto == items.IdProduto);
        }

        private void CalcularQuantidadePedido()
        {
            QuantidadeTotal = _pedidoItems.Sum(i => i.Quantidade);
        }

        public void TornarPedidoRascunho()
        {
            StatusPedido = StatusPedido.Rascunho;
        }

        public void SolicitarPedido()
        {
            StatusPedido = StatusPedido.Solicitado;
        }

        public void AprovarPedido()
        {
            StatusPedido = StatusPedido.Aprovado;
        }

        public void FalaReposicaoPedido()
        {
            StatusPedido = StatusPedido.FalhaReposicao;
        }

        public void CancelarPedido()
        {
            StatusPedido = StatusPedido.Cancelado;
        }
    }
}
