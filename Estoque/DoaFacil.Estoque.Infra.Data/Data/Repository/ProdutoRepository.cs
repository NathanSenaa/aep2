using DoaFacil.Estoque.Domain.Interfaces.Infra;
using DoaFacil.Estoque.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DoaFacil.Estoque.Infra.Data.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ProdutoContext db) : base(db)
        {
        }

        public async Task<Produto> ObterPorCodigo(string codigo)
        {
            return await Db.Produtos.FirstOrDefaultAsync(p => p.Codigo.Equals(codigo));
        }
    }
}
