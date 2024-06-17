using DoaFacil.Pedidos.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoaFacil.Pedidos.Infra.Data.Data.Mappings
{
    public class ItemsPedidoMappingss : IEntityTypeConfiguration<ItemsPedido>
    {
        public void Configure(EntityTypeBuilder<ItemsPedido> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ProdutoNome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Quantidade)
                .IsRequired()
                .HasColumnType("int");

            builder.HasOne(c => c.PedidoDoacao)
              .WithMany(c => c.PedidoItems);

            builder.ToTable("items_pedido");
        }
    }
}
