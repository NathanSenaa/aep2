using DoaFacil.IntegrationTest.Config;
using DoaFacil.Pedidos.Application.Commands.DTO;
using System.Net.Http.Json;
using Xunit;

namespace DoaFacil.IntegrationTest
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class PedidoApiTests
    {
        private readonly IntegrationTestsFixture<Program> _testsFixture;

        public PedidoApiTests(IntegrationTestsFixture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar item em novo pedido")]
        [Trait("Categoria", "Integração API - Pedido")]
        public async Task AdicionarItem_NovoPedido_DeveRetornarComSucesso()
        {
            //Arrange
            var itemPedido = new ItemPedidoDTO
            {
                IdPedido = Guid.Parse("8EFA0B84-BB7A-43C0-8FF7-8621B834C594"),
                IdProduto = Guid.Parse("C1B30CF5-6F3E-4199-BA6C-06614BD477FF"),
                Quantidade = 2
            };

            //Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("pedido/AdicionarItemPedido", itemPedido);

            // Assert
            postResponse.EnsureSuccessStatusCode();

        }
    }
}
