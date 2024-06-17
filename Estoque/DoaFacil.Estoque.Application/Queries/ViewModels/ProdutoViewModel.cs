namespace DoaFacil.Estoque.Application.Queries.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Codigo { get; private set; }
        public int Categoria { get; private set; }
        public int Quantidade { get; private set; }
    }
}
