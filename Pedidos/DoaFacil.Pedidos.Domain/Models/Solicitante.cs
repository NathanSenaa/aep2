using DoaFacil.Core.DomainObjects;

namespace DoaFacil.Pedidos.Domain.Models
{
    public class Solicitante : Entity
    {
        public Solicitante() { }
        public Solicitante(string nome, string endereco, string telefone, string email, string cnpj)
        {
            Nome = nome;
            Endereco = endereco;
            Telefone = telefone;
            Email = email;
            Cnpj = CNPJUtil.ValidarCNPJ(cnpj) ? cnpj : throw new DomainException("CNPJ inválido!!");
        }
        public string Cnpj { get; private set; }
        public string Nome { get; private set; }
        public string Endereco { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public List<PedidoDoacao> PedidosDoacao { get; private set; }

    }

}
