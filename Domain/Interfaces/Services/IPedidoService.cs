using Business.Models;
using Business.Models.Dtos;

namespace Business.Interfaces.Services
{
    public interface  IPedidoService 
    {
        Task<PedidoDTO> AdicionarAsync(PedidoDTO pedido);
        Task Remover(int id);
        

    }
}
