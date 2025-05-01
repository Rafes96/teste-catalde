using Business.Models;
using Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
       : base(options)
        {
        }


        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PedidoMapping());
            modelBuilder.ApplyConfiguration(new OcorrenciaMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
