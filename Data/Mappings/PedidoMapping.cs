using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {

            builder.ToTable("Pedidos");


            builder.HasKey(p => p.Id);


            builder.Property(p => p.NumeroPedido)
                .IsRequired();

            builder.HasIndex(p => p.NumeroPedido)
                .IsUnique();


            builder.Property(p => p.HoraPedido)
                .IsRequired();


            builder.Property(p => p.IndEntregue)
                .IsRequired();


            builder.HasMany(p => p.Ocorrencias)
                   .WithOne(o => o.Pedido)
                   .HasPrincipalKey(p => p.NumeroPedido)
                   .HasForeignKey(o => o.NumeroPedido)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
