using AutoMapper;
using DoaFacil.Estoque.Application.Commands.Produtos;
using DoaFacil.Estoque.Application.Queries.ViewModels;
using DoaFacil.Estoque.Domain.Models;

namespace DoaFacil.Estoque.Infra.Ioc
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<AdicionarProdutoCommand, Produto>();

            CreateMap<Produto, ProdutoViewModel>();
        }
    }
}
