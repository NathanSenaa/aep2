using DoaFacil.Pedidos.Application.Commands.Pedidos;
using Xunit;

namespace DoaFacil.UnityTest.Application.Pedidos
{
    public class AdicionarItemPedidoCommandTests
    {
        [Fact(DisplayName = "Adicionar Item Command Válido")]
        [Trait("Categoria", "Pedidos - Pedido Commands")]
        public void AdicionarItemPedidoCommand_CommandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(),
                Guid.NewGuid(), 3, "Produto 2");

            // Act

            var result = pedidoCommand.EhValido();

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Item Command Inválido")]
        [Trait("Categoria", "Vendas - Pedido Commands")]
        public void AdicionarItemPedidoCommand_CommandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.Empty,
                Guid.Empty, 0, "");

            // Act
            var result = pedidoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains("Id Pedido não informado", pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("Id Produto não informado", pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("Nome do produto não informado", pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("Quantidade deve ser maior que 0!", pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }

        [Fact(DisplayName = "Adicionar Item Command unidades Invalida")]
        [Trait("Categoria", "Pedidos - Pedido Commands")]
        public void AdicionarItemPedidoCommand_QuantidadeUnidadesInvalida_NaoDevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(),
                Guid.NewGuid(), 0, "Produto 2");

            // Act

            var result = pedidoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains("Quantidade deve ser maior que 0!", pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
