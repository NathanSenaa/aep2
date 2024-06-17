using DoaFacil.Core.Data;
using DoaFacil.Estoque.Domain.Models;

namespace DoaFacil.Estoque.Domain.Interfaces.Infra
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> ObterPorCodigo(string Codigo);
    }
}
