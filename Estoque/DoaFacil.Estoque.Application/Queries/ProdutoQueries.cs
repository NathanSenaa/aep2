using AutoMapper;
using DoaFacil.Estoque.Application.Queries.ViewModels;
using DoaFacil.Estoque.Domain.Interfaces.Infra;

namespace DoaFacil.Estoque.Application.Queries
{
    public class ProdutoQueries : IProdutoQueries
    {

        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoQueries(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<ProdutoViewModel> ObterProdutosPorId(Guid produtoId)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);
            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);

            return produtoViewModel;
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodosProdutos()
        {
            var produtos = await _produtoRepository.ObterTodos();
            var produtosViewModel = _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);

            return produtosViewModel;
        }
    }
}
