using DoaFacil.Pedidos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoaFacil.Pedidos.Infra.Data.Data.Mappings
{
    public class SolicitanteMappings : IEntityTypeConfiguration<Solicitante>
    {
        public void Configure(EntityTypeBuilder<Solicitante> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Telefone)
                .IsRequired()
                .HasColumnType("varchar(13)");

            builder.Property(c => c.Cnpj)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Endereco)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.HasMany(c => c.PedidosDoacao)
                .WithOne(c => c.Solicitante)
                .HasForeignKey(c => c.IdSolicitante);

            builder.ToTable("solicitantes");
        }
    }
}
