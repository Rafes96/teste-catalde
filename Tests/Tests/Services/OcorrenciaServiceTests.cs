using AutoMapper;
using Business.Interfaces.Repositorys;
using Business.Interfaces.Services;
using Business.Models;
using Business.Models.Dtos;
using Business.Models.Enums;
using Business.Notificacoes;
using Business.Services;
using FluentAssertions;
using Moq;
namespace Tests.Tests.Services
{
    public class OcorrenciaServiceTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepoMock;
        private readonly Mock<IOcorrenciaRepository> _ocorrenciaRepoMock;
        private readonly Mock<INotificador> _notificadorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly OcorrenciaService _service;


        public OcorrenciaServiceTests()
        {
            _pedidoRepoMock = new Mock<IPedidoRepository>();
            _ocorrenciaRepoMock = new Mock<IOcorrenciaRepository>();
            _notificadorMock = new Mock<INotificador>();
            _mapperMock = new Mock<IMapper>();

            _service = new OcorrenciaService(_notificadorMock.Object, _pedidoRepoMock.Object, _ocorrenciaRepoMock.Object, _mapperMock.Object);

        }

        [Fact]
        public async Task AdicionarAsync_DeveAdicionarOcorrenciaValida_ComSucesso()
        {
            // Arrange
            var pedido = new Pedido(1234);

            // Adiciona uma ocorrência anterior (para ativar o comportamento do método)
            pedido.AdicionarOcorrencia(new Ocorrencia(
                ETipoOcorrencia.ClienteAusente,
                DateTime.Now.AddMinutes(-20))
            );

            _pedidoRepoMock.Setup(r => r.ObterPorNumeroPedidoAsync(1234))
                           .ReturnsAsync(pedido);

            var dto = new OcorrenciaDTO
            {
                TipoOcorrencia = ETipoOcorrencia.EmRotaDeEntrega,
                HoraOcorrencia = DateTime.Now,
                NumeroPedido = 1234
            };

            var mappedDto = new OcorrenciaDTO
            {
                TipoOcorrencia = dto.TipoOcorrencia,
                HoraOcorrencia = dto.HoraOcorrencia,
                NumeroPedido = dto.NumeroPedido
            };

            _mapperMock.Setup(m => m.Map<OcorrenciaDTO>(It.IsAny<Ocorrencia>()))
                       .Returns(mappedDto);

            // Act
            var resultado = await _service.AdicionarAsync(dto);

            // Assert
            resultado.Should().NotBeNull();
            resultado.TipoOcorrencia.Should().Be(dto.TipoOcorrencia);
            _ocorrenciaRepoMock.Verify(r => r.Adicionar(It.IsAny<Ocorrencia>()), Times.Once);
            _pedidoRepoMock.Verify(r => r.Atualizar(It.IsAny<Pedido>()), Times.Once);
            _notificadorMock.Verify(n => n.Adicionar(It.IsAny<Notificacao>()), Times.Never);
        }

        [Fact]
        public async Task AdicionarAsync_DeveFalhar_SeOcorrenciaForRepetidaEmMenosDe10Minutos()
        {
            // Arrange
            var horaOcorrencia = DateTime.Now;
            var pedido = new Pedido(1234);
            pedido.AdicionarOcorrencia(new Ocorrencia(ETipoOcorrencia.EmRotaDeEntrega, horaOcorrencia.AddMinutes(-5)));

            _pedidoRepoMock.Setup(r => r.ObterPorNumeroPedidoAsync(1234))
                           .ReturnsAsync(pedido);

            var dto = new OcorrenciaDTO
            {
                TipoOcorrencia = ETipoOcorrencia.EmRotaDeEntrega, // mesma do mockado
                HoraOcorrencia = horaOcorrencia,
                NumeroPedido = 1234
            };

            // Act
            var resultado = await _service.AdicionarAsync(dto);

            // Assert
            resultado.Should().BeNull();
            _ocorrenciaRepoMock.Verify(r => r.Adicionar(It.IsAny<Ocorrencia>()), Times.Never);
            _notificadorMock.Verify(n => n.Adicionar(It.IsAny<Notificacao>()), Times.Once);
        }

        [Fact]
        public async Task RemoverAsync_DeveRetornarErro_SePedidoEstiverFinalizado()
        {
            // Arrange
            var pedido = new Pedido(1234);
            pedido.Finalizar(true);

            var ocorrencia = new Ocorrencia(ETipoOcorrencia.EntregueComSucesso, DateTime.Now);
            pedido.AdicionarOcorrencia(ocorrencia);
            ocorrencia.Pedido = pedido;

            _ocorrenciaRepoMock.Setup(r => r.ObterPorId(It.IsAny<int>()))
                               .ReturnsAsync(ocorrencia);

            // Act
            var resultado = await _service.RemoverAsync(1);

            // Assert
            resultado.Should().BeFalse();
            _ocorrenciaRepoMock.Verify(r => r.Remover(It.IsAny<int>()), Times.Never);
            var notificacao = _notificadorMock.Invocations
             .Select(i => i.Arguments.FirstOrDefault() as Notificacao)
             .FirstOrDefault(n => n?.Mensagem == "Impossivel excluir uma ocorrencia de um pedido finalizado.");

        }


    }
}
