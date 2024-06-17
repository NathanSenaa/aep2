using DoaFacil.Estoque.Application.Commands.Produtos;
using DoaFacil.Estoque.Application.Queries;
using DoaFacil.Estoque.Domain.Interfaces.Infra;
using DoaFacil.Estoque.Domain.Services;
using DoaFacil.Estoque.Infra.Data.Data;
using DoaFacil.Estoque.Infra.Data.Data.Repository;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DoaFacil.Estoque.Infra.Ioc
{
    public static class LocInicializeEstoque
    {

        public static void RegisterServicesEstoque(this IServiceCollection services)
        {
            // Commands
            services.AddScoped<IRequestHandler<AdicionarProdutoCommand, ValidationResult>, ProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarItemProdutoCommand, ValidationResult>, ProdutoCommandHandler>();

            //Services
            
            services.AddScoped<IEstoqueService, EstoqueService>();

            //Repository
            services.AddScoped<ProdutoContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            // Application
            services.AddScoped<IProdutoQueries, ProdutoQueries>();
        }
    }
}
