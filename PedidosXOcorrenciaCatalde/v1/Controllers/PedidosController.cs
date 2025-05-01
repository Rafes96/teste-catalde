using AutoMapper;
using Business.Interfaces.Repositorys;
using Business.Interfaces.Services;
using Business.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PedidosXOcorrenciaCatalde.Controllers;

namespace Api.v1.Controllers
{

    [Authorize]
    [Route("api/v1/pedidos")]
    public class PedidosController : MainController
    {
        private readonly IPedidoService _pedidoService;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;
        public PedidosController(INotificador notificador,
            IPedidoService pedidoService,
            IPedidoRepository pedidoRepository,
            IMapper mapper) : base(notificador)
        {
            _pedidoService = pedidoService;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }





        
        

        [HttpGet]
        [ProducesResponseType(typeof(PedidoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var pedidos = _mapper.Map<IEnumerable<PedidoDTO>>( await _pedidoRepository.ObterTodos());

            return CustomResponse(pedidos);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PedidoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var pedidos = _mapper.Map<PedidoDTO>(await _pedidoRepository.ObterPorId(id));

            return CustomResponse(pedidos);
        }


        [HttpPost]
        [ProducesResponseType(typeof(PedidoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] PedidoDTO pedido)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _pedidoService.AdicionarAsync(pedido));
        }

        
    }
}
