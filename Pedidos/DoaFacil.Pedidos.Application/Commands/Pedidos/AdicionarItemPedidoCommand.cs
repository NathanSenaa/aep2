using DoaFacil.Core.Messages;
using FluentValidation;
using FluentValidation.Results;

namespace DoaFacil.Pedidos.Application.Commands.Pedidos
{
    public class AdicionarItemPedidoCommand : Command
    {
        public AdicionarItemPedidoCommand(Guid idPedido, Guid idProduto, int quantidade, string produtoNome)
        {
            IdPedido = idPedido;
            IdProduto = idProduto;
            Quantidade = quantidade;
            ProdutoNome = produtoNome;
        }
        public Guid IdPedido { get; private set; }
        public Guid IdProduto { get; private set; }
        public int Quantidade { get; private set; }
        public string ProdutoNome { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarItemPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarItemPedidoValidation : AbstractValidator<AdicionarItemPedidoCommand>
    {
        public AdicionarItemPedidoValidation()
        {
            RuleFor(c => c.IdPedido)
                .NotEmpty()
                .WithMessage("Id Pedido não informado");

            RuleFor(c => c.IdPedido)
                .NotEmpty()
                .WithMessage("Nome do produto não informado");

            RuleFor(c => c.IdProduto)
               .NotEmpty()
               .WithMessage("Id Produto não informado");

            RuleFor(c => c.Quantidade)
              .GreaterThanOrEqualTo(1)
               .WithMessage("Quantidade deve ser maior que 0!");
        }
    }
}
