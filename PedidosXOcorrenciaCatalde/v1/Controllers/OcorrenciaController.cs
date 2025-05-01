using Business.Interfaces.Services;
using Business.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PedidosXOcorrenciaCatalde.Controllers;

namespace Api.v1.Controllers
{

    [Authorize]
    [Route("api/v1/ocorrencias")]
    public class OcorrenciaController : MainController
    {
        private readonly IOcorrenciaService _ocorrenciaService;
        public OcorrenciaController(
            INotificador notificador, 
            IOcorrenciaService ocorrenciaService) : base(notificador)
        {
            _ocorrenciaService = ocorrenciaService;
        }


        [HttpPost]
        [ProducesResponseType(typeof(OcorrenciaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] OcorrenciaDTO ocorrenciaDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            return CustomResponse(await _ocorrenciaService.AdicionarAsync(ocorrenciaDTO));
        }
            



        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
            => CustomResponse(await _ocorrenciaService.RemoverAsync(id));

        [HttpGet("obter-ocorrencia-por-numero-pedido/{numeroPedido:int}")]
        [ProducesResponseType(typeof(IEnumerable<OcorrenciaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByNumeroPedido(int numeroPedido)
            => CustomResponse(await _ocorrenciaService.ObterPorNumeroPedidoAsync(numeroPedido));

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(OcorrenciaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
            => CustomResponse(await _ocorrenciaService.ObterByIdAsync(id));



    }
}
