using DoaFacil.Core.Messages;
using DoaFacil.Core.Messages.DomainEvents;
using DoaFacil.Core.Messages.Notifications;
using FluentValidation.Results;

namespace DoaFacil.Core.Communication
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
        Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent;
    }
}