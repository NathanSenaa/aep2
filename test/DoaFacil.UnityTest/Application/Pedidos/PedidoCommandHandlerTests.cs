using DoaFacil.Pedidos.Application.Commands.Pedidos;
using DoaFacil.Pedidos.Domain.Interfaces.Infra;
using DoaFacil.Pedidos.Domain.Models;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace DoaFacil.UnityTest.Application.Pedidos
{

    public class PedidoCommandHandlerTests
    {
        private readonly Guid _pedidoId;
        private readonly Guid _produtoId;
        private readonly AutoMocker _mocker;
        private readonly PedidoCommandHandler _pedidoHandler;
        private readonly PedidoDoacao _pedido;
        public PedidoCommandHandlerTests()
        {
            _pedidoId = Guid.NewGuid();
            _produtoId = Guid.NewGuid();
            _mocker = new AutoMocker();
            _pedidoHandler = _mocker.CreateInstance<PedidoCommandHandler>();
            _pedido = new PedidoDoacao(Guid.NewGuid());
            _pedido.TornarPedidoRascunho();
            _mocker.GetMock<IPedidoDoacaoRepository>().Setup(r => r.ObterPedidoPorId(_pedidoId)).Returns(Task.FromResult(_pedido));
        }

        [Fact(DisplayName = "Adicionar Item Novo Pedido com Sucesso")]
        [Trait("Categoria", "Pedido - Pedido Command Handler")]
        public async Task AdicionarItem_NovoPedido_DeveExecutarComSucesso()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(_pedidoId,
                Guid.NewGuid(), 2, "Produto Teste");

            _mocker.GetMock<IPedidoDoacaoRepository>().Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IPedidoDoacaoRepository>().Verify(r => r.Atualizar(It.IsAny<PedidoDoacao>()), Times.Once);
            _mocker.GetMock<IPedidoDoacaoRepository>().Verify(r => r.AdicionarItem(It.IsAny<ItemsPedido>()), Times.Once);
            _mocker.GetMock<IPedidoDoacaoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Item Pedido com Sucesso")]
        [Trait("Categoria", "Pedido - Pedido Command Handler")]
        public async Task AdicionarItem_Pedido_DeveExecutarComSucesso()
        {
            // Arrange
            var itemPedido = new ItemsPedido(_pedidoId, Guid.NewGuid(), "Produto Teste", 2);
            _pedido.AdicionarItemPedido(itemPedido);

            var pedidoCommand = new AdicionarItemPedidoCommand(_pedidoId,
                Guid.NewGuid(), 5, "Produto Teste 3");

            _mocker.GetMock<IPedidoDoacaoRepository>().Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
            Assert.Equal(7, _pedido.QuantidadeTotal);
            Assert.Equal(2, _pedido.PedidoItems.Count);
            _mocker.GetMock<IPedidoDoacaoRepository>().Verify(r => r.Atualizar(It.IsAny<PedidoDoacao>()), Times.Once);
            _mocker.GetMock<IPedidoDoacaoRepository>().Verify(r => r.AdicionarItem(It.IsAny<ItemsPedido>()), Times.Once);
            _mocker.GetMock<IPedidoDoacaoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Item Pedido com Sucesso")]
        [Trait("Categoria", "Pedido - Pedido Command Handler")]
        public async Task AtualizarItem_Pedido_DeveExecutarComSucesso()
        {
            // Arrange
            var itemPedido = new ItemsPedido(_produtoId, _pedidoId, "Produto Teste", 2);
            _pedido.AdicionarItemPedido(itemPedido);

            var pedidoCommand = new AdicionarItemPedidoCommand(_pedidoId,
                _produtoId, 9, "Produto Teste");

            _mocker.GetMock<IPedidoDoacaoRepository>().Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
            Assert.Equal(11, _pedido.QuantidadeTotal);
            Assert.Single(_pedido.PedidoItems);
            _mocker.GetMock<IPedidoDoacaoRepository>().Verify(r => r.Atualizar(It.IsAny<PedidoDoacao>()), Times.Once);
            _mocker.GetMock<IPedidoDoacaoRepository>().Verify(r => r.AtualizarItem(It.IsAny<ItemsPedido>()), Times.Once);
            _mocker.GetMock<IPedidoDoacaoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}
