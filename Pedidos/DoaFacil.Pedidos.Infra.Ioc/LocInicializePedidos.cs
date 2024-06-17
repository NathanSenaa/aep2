using DoaFacil.Pedidos.Application.Commands.Pedidos;
using DoaFacil.Pedidos.Application.Commands.Solicitantes;
using DoaFacil.Pedidos.Domain.Interfaces.Infra;
using DoaFacil.Pedidos.Infra.Data.Data;
using DoaFacil.Pedidos.Infra.Data.Data.Repository;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DoaFacil.Pedidos.Infra.Ioc
{
    public static class LocInicializePedidos
    {

        public static void RegisterServicesPedidos(this IServiceCollection services)
        {

            services.AddScoped<IRequestHandler<AdicionarSolicitanteCommand, ValidationResult>, SolicitanteCommandHandler>();
            services.AddScoped<IRequestHandler<PedidoCommand, ValidationResult>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, ValidationResult>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<SolicitarPedidoCommand, ValidationResult>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<PedidoAprovadoCommand, ValidationResult>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<PedidoReprovadoCommand, ValidationResult>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarPedidoCommand, ValidationResult>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<ReposicaoProdutoFalhouCommand, ValidationResult>, PedidoCommandHandler>();

            //Repository
            services.AddScoped<PedidosContext>();
            services.AddScoped<ISolicitanteRepository, SolicitanteRepository>();
            services.AddScoped<IPedidoDoacaoRepository, PedidoDoacaoRepository>();

            // Application
        }
    }
}
