using AutoMapper;
using Business.Interfaces.Repositorys;
using Business.Interfaces.Services;
using Business.Models;
using Business.Models.Dtos;
using Business.Models.Enums;

namespace Business.Services
{
    public class OcorrenciaService : BaseService, IOcorrenciaService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IOcorrenciaRepository _ocorrenciaRepository;
        private readonly IMapper _mapper;


        public OcorrenciaService(
            INotificador notificador,
            IPedidoRepository pedidoRepository,
            IOcorrenciaRepository ocorrenciaRepository,
            IMapper mapper) : base(notificador)
        {
            _pedidoRepository = pedidoRepository;
            _ocorrenciaRepository = ocorrenciaRepository;
            _mapper = mapper;
        }


        public async Task<OcorrenciaDTO> AdicionarAsync(OcorrenciaDTO dto)
        {
            var pedido = await _pedidoRepository.ObterPorNumeroPedidoAsync(dto.NumeroPedido);
            if (pedido == null)
            {
                Notificar("Pedido não encontrado.");
                return null;
            }

            if (pedido.IndEntregue)
            {
                Notificar("Não é possível adicionar nova ocorrência. O pedido já foi finalizado.");
                return null;
            }

            var ocorrencias = pedido.Ocorrencias.OrderByDescending(o => o.HoraOcorrencia).ToList();

            var repetida = ocorrencias.FirstOrDefault(o =>
                o.TipoOcorrencia == dto.TipoOcorrencia &&
                (dto.HoraOcorrencia - o.HoraOcorrencia).TotalMinutes < 10);

            if (repetida != null)
            {
                Notificar("Não é permitido cadastrar duas ocorrências do mesmo tipo em menos de 10 minutos.");
                return null;
            }

            var novaOcorrencia = new Ocorrencia(dto.TipoOcorrencia, dto.HoraOcorrencia);

            if (ocorrencias.Count == 1)
            {
                novaOcorrencia.MarcarComoFinalizadora();
                pedido.Finalizar(novaOcorrencia.TipoOcorrencia == ETipoOcorrencia.EntregueComSucesso);
            }

            pedido.AdicionarOcorrencia(novaOcorrencia);

            await _ocorrenciaRepository.Adicionar(novaOcorrencia);
            await _pedidoRepository.Atualizar(pedido);

            return _mapper.Map<OcorrenciaDTO>(novaOcorrencia);
        }

        public async Task<OcorrenciaDTO> ObterByIdAsync(int id)
        {
            var ocorrencia = await _ocorrenciaRepository.ObterPorId(id);
            if(ocorrencia == null)
            {
                Notificar("Ocorrencia não encontrada");
                return null;
            }
            return _mapper.Map<OcorrenciaDTO>(ocorrencia);
        }

        public async Task<IEnumerable<OcorrenciaDTO>> ObterPorNumeroPedidoAsync(int numeroPedido)
        {
            var pedido = await _pedidoRepository.ObterPorNumeroPedidoAsync(numeroPedido);

            if (pedido == null)
            {
                Notificar("Pedido não encontrado.");
                return Enumerable.Empty<OcorrenciaDTO>();
            }

            return _mapper.Map<IEnumerable<OcorrenciaDTO>>(pedido.Ocorrencias);
        }

        public async Task<bool> RemoverAsync(int idOcorrencia)
        {
            var ocorrencia = await _ocorrenciaRepository.ObterPorId(idOcorrencia);

            if (ocorrencia == null)
            {
                Notificar("Ocorrência não encontrada.");
                return false;
            }

            var pedido = await _pedidoRepository.ObterPorNumeroPedidoAsync(ocorrencia.NumeroPedido);

            if (pedido == null)
            {
                Notificar("Pedido vinculado à ocorrência não encontrado.");
                return false;
            }

            if (pedido.IndEntregue)
            {
                Notificar("Impossivel excluir uma ocorrencia de um pedido finalizado.");
                return false;
            }

            await _ocorrenciaRepository.Remover(idOcorrencia);

            return true;
        }
    }
}
