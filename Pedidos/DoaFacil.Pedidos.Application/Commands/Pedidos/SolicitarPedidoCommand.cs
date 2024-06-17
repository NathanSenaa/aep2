using DoaFacil.Core.Messages;
using FluentValidation;

namespace DoaFacil.Pedidos.Application.Commands.Pedidos
{
    public class SolicitarPedidoCommand : Command
    {
        public SolicitarPedidoCommand(Guid idPedido)
        {
            IdPedido = idPedido;
        }

        public Guid IdPedido { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new SolicitarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class SolicitarPedidoValidation : AbstractValidator<SolicitarPedidoCommand>
    {
        public SolicitarPedidoValidation()
        {
            RuleFor(c => c.IdPedido)
                .NotEmpty()
                .WithMessage("Id Pedido não informado");
        }
    }
}
