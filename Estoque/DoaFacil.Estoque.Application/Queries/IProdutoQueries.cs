using DoaFacil.Estoque.Application.Queries.ViewModels;

namespace DoaFacil.Estoque.Application.Queries
{
    public interface IProdutoQueries
    {
        Task<ProdutoViewModel> ObterProdutosPorId(Guid produtoId);
        Task<IEnumerable<ProdutoViewModel>> ObterTodosProdutos();
    }
}
