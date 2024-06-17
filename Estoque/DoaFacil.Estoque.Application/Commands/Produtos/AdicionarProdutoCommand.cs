using DoaFacil.Core.Messages;
using FluentValidation;

namespace DoaFacil.Estoque.Application.Commands.Produtos
{
    public class AdicionarProdutoCommand : Command
    {
        public AdicionarProdutoCommand(string nome, int categoria, int quantidade, string codigo)
        {
            Nome = nome;
            Categoria = categoria;
            Quantidade = quantidade;
            Codigo = codigo;
        }

        public string Nome { get; private set; }
        public string Codigo { get; private set; }
        public int Categoria { get; private set; }
        public int Quantidade { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarProdutoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarProdutoValidation : AbstractValidator<AdicionarProdutoCommand>
    {
        public AdicionarProdutoValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("Nome do Produto não informado");

            RuleFor(c => c.Codigo)
                .NotEmpty()
                .WithMessage("Codigo do Produto não informado");

            RuleFor(c => c.Quantidade)
                .GreaterThanOrEqualTo(0)
                .WithMessage("A quantidade minima de um item é 0");

            RuleFor(c => c.Categoria)
                .InclusiveBetween(1, 2)
                .WithMessage("A categoria do produto deve ser 1 para Peso ou 2 para Unidade.");
        }
    }
}
