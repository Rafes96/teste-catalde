using Business.Models.Dtos;

namespace Business.Interfaces.Services
{
    public interface  IOcorrenciaService
    {
        Task<OcorrenciaDTO> AdicionarAsync(OcorrenciaDTO dto);

        Task<bool> RemoverAsync(int idOcorrencia);

        Task<IEnumerable<OcorrenciaDTO>> ObterPorNumeroPedidoAsync(int numeroPedido);
        Task<OcorrenciaDTO> ObterByIdAsync(int id);
    }
}
