using AutoMapper;
using DoaFacil.Core.DomainObjects.DTO;
using DoaFacil.Core.Messages;
using DoaFacil.Core.Messages.IntegrationEvents;
using DoaFacil.Pedidos.Domain.Interfaces.Infra;
using DoaFacil.Pedidos.Domain.Models;
using FluentValidation.Results;
using MediatR;

namespace DoaFacil.Pedidos.Application.Commands.Pedidos
{
    public class PedidoCommandHandler : CommandHandler,
        IRequestHandler<PedidoCommand, ValidationResult>,
        IRequestHandler<AdicionarItemPedidoCommand, ValidationResult>,
        IRequestHandler<SolicitarPedidoCommand, ValidationResult>,
        IRequestHandler<PedidoAprovadoCommand, ValidationResult>,
        IRequestHandler<PedidoReprovadoCommand, ValidationResult>,
        IRequestHandler<CancelarPedidoCommand, ValidationResult>,
        IRequestHandler<ReposicaoProdutoFalhouCommand, ValidationResult>
    {
        private readonly IMapper _mapper;
        private readonly ISolicitanteRepository _solicitanteRepository;
        private readonly IPedidoDoacaoRepository _pedidoDoacaoRepository;

        public PedidoCommandHandler(IMapper mapper, ISolicitanteRepository solicitanteRepository, IPedidoDoacaoRepository pedidoDoacaoRepository)
        {
            _mapper = mapper;
            _solicitanteRepository = solicitanteRepository;
            _pedidoDoacaoRepository = pedidoDoacaoRepository;
        }

        public async Task<ValidationResult> Handle(PedidoCommand message, CancellationToken cancellationToken)
        {
            // Validação do comando
            if (!message.EhValido()) return message.ValidationResult;

            var solicitante = await _solicitanteRepository.ObterPorId(message.IdSolicitante);

            if (solicitante == null)
            {
                AdicionarErro("Solicitante não localizado");
                return ValidationResult;
            }

            var pedido = _mapper.Map<PedidoDoacao>(message);

            //Tornar o pedido rascunho
            pedido.TornarPedidoRascunho();

            await _pedidoDoacaoRepository.Adicionar(pedido);

            return await PersistirDados(_pedidoDoacaoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            // Validação do comando
            if (!message.EhValido()) return message.ValidationResult;         

            var pedido = await _pedidoDoacaoRepository.ObterPedidoPorId(message.IdPedido);

            if (pedido == null)
            {
                AdicionarErro("Pedido não localizado!");
                return ValidationResult;
            }

            if (pedido.StatusPedido != StatusPedido.Rascunho)
            {
                AdicionarErro("Para adicionar um item ao pedido ele deve estar em rascunho!");
                return ValidationResult;
            }

            var item = new ItemsPedido(message.IdProduto, message.IdPedido, message.ProdutoNome, message.Quantidade);

            var itemExistente = pedido.PedidoItemExistente(item);
            pedido.AdicionarItemPedido(item);

            if (itemExistente)
            {
                await _pedidoDoacaoRepository.AtualizarItem(pedido.PedidoItems.FirstOrDefault(i => i.IdProduto == item.IdProduto));
            }
            else
            {
                await _pedidoDoacaoRepository.AdicionarItem(item);
            }

            await _pedidoDoacaoRepository.Atualizar(pedido);

            return await PersistirDados(_pedidoDoacaoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(SolicitarPedidoCommand message, CancellationToken cancellationToken)
        {
            // Validação do comando
            if (!message.EhValido()) return message.ValidationResult;

            //Busca o pedido na base de dados
            var pedido = await _pedidoDoacaoRepository.ObterPedidoPorId(message.IdPedido);

            //Realiza algumas validações nesse pedido
            if (!PedidoSolicitadoValido(pedido)) return ValidationResult;


            var itemsList = new List<Item>();

            //Popula a itemsList de acordo com os items viculados ao pedido 
            pedido.PedidoItems.ToList().ForEach(item => itemsList.Add(new Item { Id = item.IdProduto, Quantidade = item.Quantidade}));

            //Adiciona o itemsList no Objeto do tipo 'ListaProdutosPedido'
            var listaProdutosPedido = new ListaProdutosPedido { PedidoId = pedido.Id, Itens = itemsList };

            //Adiciona um evento do tipo 'PedidoIniciadoEvent'
            pedido.AdicionarEvento(new PedidoIniciadoEvent(pedido.Id, pedido.IdSolicitante, listaProdutosPedido));

            //Muda o status do Pedido para Solicitado
            pedido.SolicitarPedido();

            //Persiste a operação na base de dados
            return await PersistirDados(_pedidoDoacaoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(PedidoAprovadoCommand message, CancellationToken cancellationToken)
        {
            // Validação do comando
            if (!message.EhValido()) return message.ValidationResult;

            var pedido = await _pedidoDoacaoRepository.ObterPedidoPorId(message.IdPedido);
            if(pedido == null)
            {
                AdicionarErro("Pedido não localizado");
                return ValidationResult;
            }

            if(pedido.StatusPedido != StatusPedido.Solicitado)
            {
                AdicionarErro("Pedido deve estar com o status 'Solicitado'!");
                return ValidationResult;
            }
            pedido.AprovarPedido();
            await _pedidoDoacaoRepository.Atualizar(pedido);
            return await PersistirDados(_pedidoDoacaoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(PedidoReprovadoCommand message, CancellationToken cancellationToken)
        {
            // Validação do comando
            if (!message.EhValido()) return message.ValidationResult;

            var pedido = await _pedidoDoacaoRepository.ObterPedidoPorId(message.IdPedido);
            if (pedido == null)
            {
                AdicionarErro("Pedido não localizado");
                return ValidationResult;
            }

            if (pedido.StatusPedido != StatusPedido.Solicitado)
            {
                AdicionarErro("Pedido deve estar com o status 'Solicitado'!");
                return ValidationResult;
            }
            pedido.TornarPedidoRascunho();
            await _pedidoDoacaoRepository.Atualizar(pedido);
            return await PersistirDados(_pedidoDoacaoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(CancelarPedidoCommand message, CancellationToken cancellationToken)
        {
            // Validação do comando
            if (!message.EhValido()) return message.ValidationResult;

            var pedido = await _pedidoDoacaoRepository.ObterPedidoPorId(message.IdPedido);

            if(pedido == null) return ValidationResult;

            //Pedido deve estar Aprovado ou em rascunho para ser cancelado
            if(pedido.StatusPedido != StatusPedido.Rascunho && pedido.StatusPedido != StatusPedido.Aprovado)
            {
                AdicionarErro("Pedido não pode ser cancelado! ");
                return ValidationResult;
            }           

            if(pedido.StatusPedido == StatusPedido.Aprovado)
            {
                //Necessário repor a quantidade em estoque na tabela de produtos
                List<Item> itemProduto = new List<Item>();

                //Popula a itemsList de acordo com os items viculados ao pedido 
                pedido.PedidoItems.ToList().ForEach(item => itemProduto.Add(new Item {Id = item.IdProduto, Quantidade = item.Quantidade }));

                //Adiciona o itemsList no Objeto do tipo 'ListaProdutosPedido'
                var listaProdutosPedido = new ListaProdutosPedido { PedidoId = pedido.Id, Itens = itemProduto };

                //Adicionar o evendo do tipo 'PedidoReprovadoEvent' para ser consumido para o dominio de produtos
                pedido.AdicionarEvento(new PedidoReprovadoEvent(pedido.Id, pedido.IdSolicitante, listaProdutosPedido));
            }

            pedido.CancelarPedido();

            await _pedidoDoacaoRepository.Atualizar(pedido);

            return await PersistirDados(_pedidoDoacaoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(ReposicaoProdutoFalhouCommand message, CancellationToken cancellationToken)
        {
            // Validação do comando
            if (!message.EhValido()) return message.ValidationResult;

             var pedido = await _pedidoDoacaoRepository.ObterPedidoPorId(message.IdPedido);
            if (pedido == null)
            {
                AdicionarErro("Pedido não localizado");
                return ValidationResult;
            }
            pedido.FalaReposicaoPedido();

            await _pedidoDoacaoRepository.Atualizar(pedido);
            return await PersistirDados(_pedidoDoacaoRepository.UnitOfWork);

        }

        private bool PedidoSolicitadoValido(PedidoDoacao pedido)
        {
            var valido = true;
            if(pedido == null)
            {
                AdicionarErro("Pedido não localizado!");
                valido = false;
            }
            if (pedido.StatusPedido != StatusPedido.Rascunho)
            {
                AdicionarErro("Pedido precisa estar em rascunho!");
                valido = false;
            }
            if(pedido.PedidoItems.Count < 1)
            {
                AdicionarErro("Pedido não contem produtos!");
                valido = false;
            }

            return valido;
        }
    }
}
