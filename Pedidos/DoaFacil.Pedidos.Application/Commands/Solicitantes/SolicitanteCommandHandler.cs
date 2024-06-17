using AutoMapper;
using DoaFacil.Core.Messages;
using DoaFacil.Pedidos.Domain.Interfaces.Infra;
using DoaFacil.Pedidos.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace DoaFacil.Pedidos.Application.Commands.Solicitantes
{
    public class SolicitanteCommandHandler : CommandHandler,
        IRequestHandler<AdicionarSolicitanteCommand, ValidationResult>
    {
        private readonly IMapper _mapper;
        private readonly ISolicitanteRepository _solicitanteRepository;

        public SolicitanteCommandHandler(IMapper mapper, ISolicitanteRepository solicitanteRepository)
        {
            _mapper = mapper;
            _solicitanteRepository = solicitanteRepository;
        }

        public async Task<ValidationResult> Handle(AdicionarSolicitanteCommand message, CancellationToken cancellationToken)
        {
            // Validação do comando
            if (!message.EhValido()) return message.ValidationResult;

            var clienteCadastrado = await _solicitanteRepository.ObterPacientePorCnpj(message.Cnpj);

            if (clienteCadastrado != null)
            {
                AdicionarErro("Solicitante já está cadastrado na base!");
                return ValidationResult;
            }

            var solicitante = _mapper.Map<Solicitante>(message);

            await _solicitanteRepository.Adicionar(solicitante);

            return await PersistirDados(_solicitanteRepository.UnitOfWork);
        }
    }
}
