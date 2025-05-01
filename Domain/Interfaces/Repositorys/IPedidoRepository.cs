using Business.Models;

namespace Business.Interfaces.Repositorys
{
    public interface  IPedidoRepository : IRepository<Pedido>
    {
        Task<Pedido> ObterPorNumeroPedidoAsync(int numeroPedido);
    }
}
