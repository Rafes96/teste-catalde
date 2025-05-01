using Business.Models.Enums;

namespace Business.Models
{
    public class Ocorrencia : BaseEntity
    {
        
        public ETipoOcorrencia TipoOcorrencia { get; set; }
        public DateTime HoraOcorrencia { get; set; }
        public bool IndFinalizadora { get; set; }

        public int NumeroPedido { get; set; }
        public Pedido Pedido { get; set; }

        public Ocorrencia(ETipoOcorrencia tipoOcorrencia, DateTime horaOcorrencia)
        {
            TipoOcorrencia = tipoOcorrencia;
            HoraOcorrencia = horaOcorrencia;
        }

        public void MarcarComoFinalizadora()
        {
            IndFinalizadora = true;
        }
    }
}
