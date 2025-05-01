using System.Text.Json.Serialization;
using Business.Models.Enums;

namespace Business.Models.Dtos
{
    public class OcorrenciaDTO
    {
        public int Id { get; private set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ETipoOcorrencia TipoOcorrencia { get; set; }
        public DateTime HoraOcorrencia { get; set; }
        public bool IndFinalizadora { get; set; }

        public int NumeroPedido { get; set; }

        [JsonIgnore] 
        public PedidoDTO? Pedido { get; set; }

    }
}
