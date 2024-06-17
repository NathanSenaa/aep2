using DoaFacil.Core.Messages;
using FluentValidation;

namespace DoaFacil.Pedidos.Application.Commands.Pedidos
{
    public class PedidoAprovadoCommand : Command
    {
        public PedidoAprovadoCommand(Guid idPedido)
        {
            IdPedido = idPedido;
        }

        public Guid IdPedido { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new PedidoAprovadoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class PedidoAprovadoValidation : AbstractValidator<PedidoAprovadoCommand>
    {
        public PedidoAprovadoValidation()
        {
            RuleFor(c => c.IdPedido)
                .NotEmpty()
                .WithMessage("Id Pedido não informado");
        }
    }
}
