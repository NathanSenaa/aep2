using DoaFacil.Core.Data;
using DoaFacil.Pedidos.Domain.Models;

namespace DoaFacil.Pedidos.Domain.Interfaces.Infra
{
    public interface ISolicitanteRepository : IRepository<Solicitante>
    {

        Task<Solicitante> ObterPacientePorCnpj(string cnpj);
    }
}
