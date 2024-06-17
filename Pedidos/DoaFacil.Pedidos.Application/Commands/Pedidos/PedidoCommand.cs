using DoaFacil.Core.Messages;
using FluentValidation;
using FluentValidation.Results;

namespace DoaFacil.Pedidos.Application.Commands.Pedidos
{
    public class PedidoCommand : Command
    {
        public PedidoCommand(Guid idSolicitante)
        {
            IdSolicitante = idSolicitante;
        }
        public Guid IdSolicitante { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new PedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class PedidoValidation : AbstractValidator<PedidoCommand>
    {
        public PedidoValidation()
        {
            RuleFor(c => c.IdSolicitante)
                .NotEmpty()
                .WithMessage("Id Solicitante não informado");
        }
    }
}
