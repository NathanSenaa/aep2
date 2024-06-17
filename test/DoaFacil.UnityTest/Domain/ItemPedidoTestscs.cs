using DoaFacil.Core.DomainObjects;
using DoaFacil.Pedidos.Domain.Models;
using Xunit;

namespace DoaFacil.UnityTest.Domain
{
    public class ItemPedidoTestscs
    {
        [Fact(DisplayName = "Adicionar Item abaixo do permitido")]
        [Trait("Categoria", "Pedido - Pedido Item")]
        public void AdicionarItemPedido_UnidadeItemNegativo_DeveRetornarException()
        {
            // Arrange & Act & Assert
            Assert.Throws<DomainException>(() => new ItemsPedido(Guid.NewGuid(), Guid.NewGuid(), "Produto 1",-1));
        }
    }
}
