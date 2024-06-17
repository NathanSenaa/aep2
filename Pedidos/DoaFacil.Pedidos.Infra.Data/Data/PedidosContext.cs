
using DoaFacil.Core.Communication;
using DoaFacil.Core.Data;
using DoaFacil.Core.DomainObjects;
using DoaFacil.Core.Messages;
using DoaFacil.Pedidos.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DoaFacil.Pedidos.Infra.Data.Data
{
    public class PedidosContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public PedidosContext(DbContextOptions<PedidosContext> options, IMediatorHandler mediatorHandler)
        : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }
        public DbSet<Solicitante> Solicitantes { get; set; }
        public DbSet<ItemsPedido> ItemsPedido { get; set; }
        public DbSet<PedidoDoacao> PedidoDoacao { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
               .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

              modelBuilder.Ignore<Event>();

            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1000).IncrementsBy(1);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidosContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            if(sucesso) await _mediatorHandler.PublicarEventos(this);
            return sucesso;
        }
    }

    public static class MediatorExtension
    {
        public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notificacoes)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublicarEvento(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
