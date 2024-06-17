using DoaFacil.Core.DomainObjects;

namespace DoaFacil.Pedidos.Domain.Models
{
    public class ItemsPedido : Entity
    {
        public ItemsPedido(Guid idProduto, Guid idPedidoDoacao, string produtoNome, int quantidade)
        {
            if (quantidade < 1) throw new DomainException("A quantidade do produto não pode ser inferior a 0");
            IdProduto = idProduto;
            IdPedidoDoacao = idPedidoDoacao;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
        }

        public void AtualizarQuantidade(int quantidade)
        {
            if (quantidade < 1) throw new DomainException("A quantidade de item não pode ser inferior a 1!!");
            Quantidade += quantidade;
        }

        public Guid IdProduto { get; private set; }
        public Guid IdPedidoDoacao { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public PedidoDoacao PedidoDoacao { get; private set; }
    }
}
