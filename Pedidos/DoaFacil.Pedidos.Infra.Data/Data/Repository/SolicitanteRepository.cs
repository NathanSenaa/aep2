using DoaFacil.Pedidos.Domain.Interfaces.Infra;
using DoaFacil.Pedidos.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DoaFacil.Pedidos.Infra.Data.Data.Repository
{
    public class SolicitanteRepository : Repository<Solicitante>, ISolicitanteRepository
    {
        public SolicitanteRepository(PedidosContext db) : base(db)
        {
        }

        public async Task<Solicitante> ObterPacientePorCnpj(string cnpj)
        {
            return await DbSet.FirstOrDefaultAsync(s => s.Cnpj == cnpj);
        }
    }
}
