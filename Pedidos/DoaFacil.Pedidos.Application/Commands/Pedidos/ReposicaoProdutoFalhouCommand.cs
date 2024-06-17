using DoaFacil.Core.Messages;
using FluentValidation;

namespace DoaFacil.Pedidos.Application.Commands.Pedidos
{
    public class ReposicaoProdutoFalhouCommand : Command
    {
        public ReposicaoProdutoFalhouCommand(Guid idPedido)
        {
            IdPedido = idPedido;
        }

        public Guid IdPedido { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new ReposicaoProdutoFalhouValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ReposicaoProdutoFalhouValidation : AbstractValidator<ReposicaoProdutoFalhouCommand>
    {
        public ReposicaoProdutoFalhouValidation()
        {
            RuleFor(c => c.IdPedido)
                .NotEmpty()
                .WithMessage("Id Pedido não informado");
        }
    }
}
