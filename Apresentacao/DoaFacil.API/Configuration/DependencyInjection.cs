using DoaFacil.Core.Communication;
using DoaFacil.Core.Messages.IntegrationEvents;
using DoaFacil.Core.Messages.Notifications;
using DoaFacil.Estoque.Domain.Event;
using DoaFacil.Pedidos.Application.Event;
using MediatR;

namespace DoaFacil.API.Configuration
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<INotificationHandler<PedidoIniciadoEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoReprovadoEvent>, ProdutoEventHandler>();

            services.AddScoped<INotificationHandler<PedidoAprovadoEvent>, PedidoEventHanlder>();
            services.AddScoped<INotificationHandler<PedidoRejeitadoEvent>, PedidoEventHanlder>();
            services.AddScoped<INotificationHandler<ReposicaoProdutosFalhouEvent>, PedidoEventHanlder>();


        }
    }
}
