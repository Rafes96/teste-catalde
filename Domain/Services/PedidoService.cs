using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces.Repositorys;
using Business.Interfaces.Services;
using Business.Models;
using Business.Models.Dtos;
using Business.Models.Validations;

namespace Business.Services
{
    public class PedidoService : BaseService, IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;
        public PedidoService(INotificador notificador, IPedidoRepository pedidoRepository, IMapper mapper) : base(notificador)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<PedidoDTO> AdicionarAsync(PedidoDTO pedido)
        {
            var newPedido = new Pedido(pedido.NumeroPedido);
            if (!ExecutarValidacao(new PedidoValidator(), newPedido)) return null;


            var pedidoExiste = await _pedidoRepository.ObterPorNumeroPedidoAsync(newPedido.NumeroPedido);
            if (pedidoExiste != null)
            {
                Notificar("Numero de pedido já existente");
                return null;
            }
            await _pedidoRepository.Adicionar(newPedido);
            
            return _mapper.Map<PedidoDTO>(newPedido) ;
        }

        public Task Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}
