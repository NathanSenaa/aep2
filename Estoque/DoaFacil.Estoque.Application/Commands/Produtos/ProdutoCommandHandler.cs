using AutoMapper;
using DoaFacil.Core.Messages;
using DoaFacil.Estoque.Domain.Interfaces.Infra;
using DoaFacil.Estoque.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace DoaFacil.Estoque.Application.Commands.Produtos
{
    public class ProdutoCommandHandler : CommandHandler,
        IRequestHandler<AdicionarProdutoCommand, ValidationResult>,
        IRequestHandler<AdicionarItemProdutoCommand, ValidationResult>
    {

        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoCommandHandler(IMapper mapper, IProdutoRepository produtoRepository
            )
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        public async Task<ValidationResult> Handle(AdicionarProdutoCommand message, CancellationToken cancellationToken)
        {
            // Validação do comando
            if (!message.EhValido()) return message.ValidationResult;


            var produto = _mapper.Map<Produto>(message);
            var produtoUpdate = await _produtoRepository.ObterPorCodigo(produto.Codigo);

            if (produtoUpdate == null)
            {
                await _produtoRepository.Adicionar(produto);
            }
            else
            {
                produtoUpdate.AdicionarQuantidade(produto.Quantidade);
                await _produtoRepository.Atualizar(produtoUpdate);
            }

            return await PersistirDados(_produtoRepository.UnitOfWork);

        }

        public async Task<ValidationResult> Handle(AdicionarItemProdutoCommand message, CancellationToken cancellationToken)
        {
            // Validação do comando
            if (!message.EhValido()) return message.ValidationResult;

            var produto = await _produtoRepository.ObterPorId(message.IdProduto);

            if (produto == null)
            {
                AdicionarErro("Produto não localizado");
                return message.ValidationResult;
            }
            produto.AdicionarQuantidade(message.Quantidade);
            await _produtoRepository.Atualizar(produto);

            return await PersistirDados(_produtoRepository.UnitOfWork);
        }
    }
}
