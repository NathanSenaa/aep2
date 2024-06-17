using DoaFacil.Core.DomainObjects;

namespace DoaFacil.Estoque.Domain.Models
{
    public class Produto : Entity
    {

        public Produto()
        {
            ValidarQuantidade();
        }
        public Produto(string nome, CategoriaProduto categoria, int quantidade)
        {
            Nome = nome;
            Categoria = categoria;
            Quantidade = quantidade;
            ValidarQuantidade();
        }

        public string Nome { get; private set; }
        public string Codigo { get; private set; }
        public CategoriaProduto Categoria { get; private set; }
        public int Quantidade { get; private set; }

        public void ReporEstoque(int quantidade)
        {
            Quantidade += quantidade;
            ValidarQuantidade();
        }

        public void DebitarEstoque(int quantidade)
        {
            Quantidade -= quantidade;
            ValidarQuantidade();
        }

        public bool TemEstoque(int quantidade)
        {
           return Quantidade > quantidade;
        }

        private void ValidarQuantidade()
        {
            if (Quantidade < 0) throw new DomainException("A quantidade minima do produto não deve ser menor que 0");
        }

        public void AdicionarQuantidade(int quantidade)
        {
            if (quantidade < 0) throw new DomainException("A quantidade minima do produto não deve ser menor que 0");
            Quantidade += quantidade;
        }
    }
}
