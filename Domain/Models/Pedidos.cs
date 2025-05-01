namespace Business.Models
{
    public class Pedido : BaseEntity
    {
        public int NumeroPedido { get; private set; }
        public DateTime HoraPedido { get; private set; }
        public bool IndEntregue { get; private set; }

        public List<Ocorrencia> Ocorrencias { get; private set; } = new();

       
        public Pedido(int numeroPedido)
        {
            NumeroPedido = numeroPedido;
            HoraPedido = DateTime.Now;
        }

        public void Finalizar(bool entregueComSucesso)
        {
            IndEntregue = entregueComSucesso;
        }


        public void AdicionarOcorrencia(Ocorrencia ocorrencia)
        {
            if (ocorrencia == null)
                throw new ArgumentNullException(nameof(ocorrencia));

            Ocorrencias.Add(ocorrencia);
        }
    }
}
