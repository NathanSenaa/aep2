using DoaFacil.Core.Messages;
using FluentValidation;

namespace DoaFacil.Pedidos.Application.Commands.Pedidos
{
    public class CancelarPedidoCommand : Command
    {
        public CancelarPedidoCommand(Guid idPedido)
        {
            IdPedido = idPedido;
        }

        public Guid IdPedido { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new CancelarValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CancelarValidation : AbstractValidator<CancelarPedidoCommand>
    {
        public CancelarValidation()
        {
            RuleFor(c => c.IdPedido)
                .NotEmpty()
                .WithMessage("Id Pedido não informado");
        }
    }
}
