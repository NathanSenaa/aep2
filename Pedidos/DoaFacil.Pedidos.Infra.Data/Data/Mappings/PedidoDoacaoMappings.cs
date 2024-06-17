using DoaFacil.Pedidos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoaFacil.Pedidos.Infra.Data.Data.Mappings
{
    public class PedidoDoacaoMappings : IEntityTypeConfiguration<PedidoDoacao>
    {
        public void Configure(EntityTypeBuilder<PedidoDoacao> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.StatusPedido)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(c => c.QuantidadeTotal)
                .HasColumnType("int");

            builder.Property(c => c.DataSolicitacao)
                .IsRequired()
                .HasColumnType("datetime");

            builder.HasOne(c => c.Solicitante)
               .WithMany(c => c.PedidosDoacao);

            builder.HasMany(c => c.PedidoItems)
                .WithOne(c => c.PedidoDoacao)
                .HasForeignKey(c => c.IdPedidoDoacao);

            builder.Property(c => c.Codigo)
                .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            builder.ToTable("pedidos_doacao");
        }
    }
}
