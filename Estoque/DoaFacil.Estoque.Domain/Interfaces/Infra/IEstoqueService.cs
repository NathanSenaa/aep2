using DoaFacil.Core.DomainObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoaFacil.Estoque.Domain.Interfaces.Infra
{
    public interface IEstoqueService : IDisposable
    {
        Task<bool> DebitarEstoque(Guid produtoId, int quantidade);
        Task<bool> DebitarListaProdutosPedido(ListaProdutosPedido lista);
        Task<bool> CreditarListaProdutosPedido(ListaProdutosPedido lista);
    }
}
