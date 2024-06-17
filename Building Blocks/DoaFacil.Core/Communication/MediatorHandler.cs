﻿using DoaFacil.Core.Messages;
using DoaFacil.Core.Messages.DomainEvents;
using DoaFacil.Core.Messages.Notifications;
using FluentValidation.Results;
using MediatR;

namespace DoaFacil.Core.Communication
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;     

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }       
        public async Task<ValidationResult> EnviarComando<T>(T comando) where T : Command
        {
            return await _mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);

        }

        public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
        {
            await _mediator.Publish(notificacao);
        }

        public async Task PublicarDomainEvent<T>(T notificacao) where T : DomainEvent
        {
            await _mediator.Publish(notificacao);
        }
    }
}