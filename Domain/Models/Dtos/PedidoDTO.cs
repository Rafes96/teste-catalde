namespace Business.Models.Dtos
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public int NumeroPedido { get;  set; }
        public DateTime HoraPedido { get; private set; }
        public bool IndEntregue { get; private set; }

        public List<OcorrenciaDTO> Ocorrencias { get; private set; } = new();
    }
}
