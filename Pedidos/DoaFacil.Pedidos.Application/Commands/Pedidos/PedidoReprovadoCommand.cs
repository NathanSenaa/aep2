using DoaFacil.Core.Messages;
using FluentValidation;

namespace DoaFacil.Pedidos.Application.Commands.Pedidos
{
    public class PedidoReprovadoCommand : Command
    {
        public PedidoReprovadoCommand(Guid idPedido)
        {
            IdPedido = idPedido;
        }

        public Guid IdPedido { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new PedidoReprovadoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class PedidoReprovadoValidation : AbstractValidator<PedidoReprovadoCommand>
    {
        public PedidoReprovadoValidation()
        {
            RuleFor(c => c.IdPedido)
                .NotEmpty()
                .WithMessage("Id Pedido não informado");
        }
    }
}
