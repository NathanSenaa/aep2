using DoaFacil.Core.DomainObjects;
using DoaFacil.Core.Messages;
using FluentValidation;

namespace DoaFacil.Pedidos.Application.Commands.Solicitantes
{
    public class AdicionarSolicitanteCommand : Command
    {
        public AdicionarSolicitanteCommand(string nome, string endereco, string telefone, string email, string cnpj)
        {
            Nome = nome;
            Endereco = endereco;
            Telefone = telefone;
            Email = email;
            Cnpj = cnpj;
        }

        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public string Endereco { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarSolicitanteValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarSolicitanteValidation : AbstractValidator<AdicionarSolicitanteCommand>
    {
        public AdicionarSolicitanteValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("Nome do Produto não informado");

            RuleFor(c => c.Endereco)
                .NotEmpty()
                .WithMessage("Endereco do Solicitante não informado");

            RuleFor(c => c.Telefone)
                .NotEmpty()
                .WithMessage("Telefone do Solicitante não informado");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("Email do Solicitante não informado");

            RuleFor(c => c.Cnpj)
                .NotEmpty().WithMessage("CNPJ do Solicitante não informado")
                .Must(CNPJUtil.ValidarCNPJ).WithMessage("CNPJ inválido");
        }
    }
}
