using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class OcorrenciaMapping : IEntityTypeConfiguration<Ocorrencia>
    {

        public void Configure(EntityTypeBuilder<Ocorrencia> builder)
        {
            
            builder.ToTable("Ocorrencias");

            
            builder.HasKey(o => o.Id);

            
            builder.Property(o => o.TipoOcorrencia)
                .IsRequired()
                .HasConversion<int>(); 

            
            builder.Property(o => o.HoraOcorrencia)
                .IsRequired();

            
            builder.Property(o => o.IndFinalizadora)
                .IsRequired();

            
            builder.Property(o => o.NumeroPedido)
                .IsRequired();
        }
    }
}