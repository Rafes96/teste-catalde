using FluentValidation;

namespace Business.Models.Validations
{
    public class PedidoValidator : AbstractValidator<Pedido>
    {
        public PedidoValidator()
        {
            RuleFor(x => x.NumeroPedido).GreaterThan(0).WithMessage("Numero do pedido precisa ser maior que 0");
        }
    }
}
