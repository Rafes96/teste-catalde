using Business.Interfaces.Repositorys;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(ApiDbContext db) : base(db)
        {
        }


        public override async Task<Pedido> ObterPorId(int id)
        {
            return await DbSet
           .Include(p => p.Ocorrencias)
           .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Pedido> ObterPorNumeroPedidoAsync(int numeroPedido)
        {
            return await DbSet
                .Include(p => p.Ocorrencias)
                .FirstOrDefaultAsync(p => p.NumeroPedido == numeroPedido);
        }
    }
}


