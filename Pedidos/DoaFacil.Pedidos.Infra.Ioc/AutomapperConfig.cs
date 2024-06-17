using AutoMapper;
using DoaFacil.Pedidos.Application.Commands.Pedidos;
using DoaFacil.Pedidos.Application.Commands.Solicitantes;
using DoaFacil.Pedidos.Domain.Models;

namespace DoaFacil.Pedidos.Infra.Ioc
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<AdicionarSolicitanteCommand, Solicitante>();
            CreateMap<PedidoCommand, PedidoDoacao>()
                .ConstructUsing(p =>
                new PedidoDoacao(p.IdSolicitante));

        }
    }
}
