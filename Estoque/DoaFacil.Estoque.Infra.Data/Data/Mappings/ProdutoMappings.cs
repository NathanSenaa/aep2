using DoaFacil.Estoque.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoaFacil.Estoque.Infra.Data.Data.Mappings
{
    public class ProdutoMappings : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasColumnType("varchar(15)");

            builder.Property(c => c.Quantidade)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Categoria)
                .IsRequired()
                .HasColumnType("int");

            builder.ToTable("produtos");
        }
    }
}
