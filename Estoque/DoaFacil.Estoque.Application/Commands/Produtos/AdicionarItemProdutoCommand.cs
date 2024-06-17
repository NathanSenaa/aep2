using DoaFacil.Core.Messages;
using FluentValidation;

namespace DoaFacil.Estoque.Application.Commands.Produtos
{
    public class AdicionarItemProdutoCommand : Command
    {
        public AdicionarItemProdutoCommand(Guid idProduto, int quantidade)
        {
            IdProduto = idProduto;
            Quantidade = quantidade;
        }

        public Guid IdProduto { get; private set; }
        public int Quantidade { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarItemProdutoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class AdicionarItemProdutoValidation : AbstractValidator<AdicionarItemProdutoCommand>
    {
        public AdicionarItemProdutoValidation()
        {
            RuleFor(c => c.Quantidade)
               .GreaterThanOrEqualTo(1)
               .WithMessage("A quantidade minima para adicionar um item é 1");

            RuleFor(c => c.IdProduto)
                .NotEmpty()
                .WithMessage("Id do Produto não foi informado");
        }
    }
}
