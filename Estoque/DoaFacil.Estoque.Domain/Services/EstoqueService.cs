using DoaFacil.Core.DomainObjects.DTO;
using DoaFacil.Estoque.Domain.Interfaces.Infra;

namespace DoaFacil.Estoque.Domain.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;

        public EstoqueService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DebitarListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach(var item in lista.Itens)
            {
                if (!await DebitarItemEstoque(item.Id, item.Quantidade)) return false;
            }
            return await _produtoRepository.UnitOfWork.Commit();
        }        

        public async Task<bool> CreditarListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach(var item in lista.Itens)
            {
                if (!await CreditarItemEstoque(item.Id, item.Quantidade)) return false;
            }
            
            return await _produtoRepository.UnitOfWork.Commit();
        }

        private async Task<bool> DebitarItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            if (!produto.TemEstoque(quantidade)) return false;

            produto.DebitarEstoque(quantidade);

            await _produtoRepository.Atualizar(produto);

            return true;
        }

        private async Task<bool> CreditarItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            produto.AdicionarQuantidade(quantidade);

            await _produtoRepository.Atualizar(produto);

            return true;
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
