using DoaFacil.Pedidos.Domain.Models;
using Xunit;

namespace DoaFacil.UnityTest.Domain
{
    public class PedidoDoacaoTests
    {
        [Fact(DisplayName = "Adicionar Item Novo Pedido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarQuantidade()
        {
            // Arrange
            var pedido = new PedidoDoacao(Guid.NewGuid());
            var itemPedido = new ItemsPedido(Guid.NewGuid(), pedido.Id, "produto 1", 10);

            //Act
            pedido.AdicionarItemPedido(itemPedido);

            //Assert
            Assert.Equal(10, pedido.QuantidadeTotal);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Existente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_NovoPedido_DeveIncrementarQuantidadePedido()
        {
            // Arrange
            var pedido = new PedidoDoacao(Guid.NewGuid());
            var itemPedido = new ItemsPedido(Guid.NewGuid(), pedido.Id, "produto 1", 10);
            pedido.AdicionarItemPedido(itemPedido);
            var itemPedido2 = new ItemsPedido(Guid.NewGuid(), pedido.Id, "produto 2", 30);

            //Act
            pedido.AdicionarItemPedido(itemPedido2);

            //Assert
            Assert.Equal(40, pedido.QuantidadeTotal);
            Assert.Equal(2, pedido.PedidoItems.Count);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Existente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_NovoPedido_DeveAtualizarQuantidadePedido()
        {
            // Arrange
            var pedido = new PedidoDoacao(Guid.NewGuid());
            var itemPedido = new ItemsPedido(Guid.NewGuid(), pedido.Id, "produto 1", 10);
            pedido.AdicionarItemPedido(itemPedido);
            var itemPedido2 = new ItemsPedido(itemPedido.IdProduto, pedido.Id, "produto 1", 30);

            //Act
            pedido.AdicionarItemPedido(itemPedido2);

            //Assert
            Assert.Equal(40, pedido.QuantidadeTotal);
            Assert.Single(pedido.PedidoItems);
        }

        [Fact(DisplayName = "Remover Item Pedido Existente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void RemoverItemPedido_NovoPedido_DeveAtualizarQuantidadePedido()
        {
            // Arrange
            var pedido = new PedidoDoacao(Guid.NewGuid());
            var itemPedido = new ItemsPedido(Guid.NewGuid(), pedido.Id, "produto 1", 10);
            pedido.AdicionarItemPedido(itemPedido);

            //Act
            pedido.RemoverItemPedido(itemPedido);

            //Assert
            Assert.Equal(0, pedido.QuantidadeTotal);
            Assert.Empty(pedido.PedidoItems);
        }

        [Fact(DisplayName = "Remover 2 Item Pedido Existente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void RemoverTodosItensPedido_NovoPedido_DeveAtualizarQuantidadePedido()
        {
            // Arrange
            var pedido = new PedidoDoacao(Guid.NewGuid());
            var itemPedido = new ItemsPedido(Guid.NewGuid(), pedido.Id, "produto 1", 10);
            pedido.AdicionarItemPedido(itemPedido);
            var itemPedido2 = new ItemsPedido(Guid.NewGuid(), pedido.Id, "produto 2", 40);
            pedido.AdicionarItemPedido(itemPedido2);

            //Act
            pedido.RemoverItemPedido(itemPedido);
            pedido.RemoverItemPedido(itemPedido2);

            //Assert
            Assert.Equal(0, pedido.QuantidadeTotal);
            Assert.Empty(pedido.PedidoItems);
        }

        [Fact(DisplayName = "Remover 1 Item Pedido Existente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void RemoverUmItemPedido_NovoPedido_DeveAtualizarQuantidadePedido()
        {
            // Arrange
            var pedido = new PedidoDoacao(Guid.NewGuid());
            var itemPedido = new ItemsPedido(Guid.NewGuid(), pedido.Id, "produto 1", 10);
            pedido.AdicionarItemPedido(itemPedido);
            var itemPedido2 = new ItemsPedido(Guid.NewGuid(), pedido.Id, "produto 2", 40);
            pedido.AdicionarItemPedido(itemPedido2);

            //Act
            pedido.RemoverItemPedido(itemPedido);

            //Assert
            Assert.Equal(40, pedido.QuantidadeTotal);
            Assert.Single(pedido.PedidoItems);
        }
    }
}
