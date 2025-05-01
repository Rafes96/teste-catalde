using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Notificacoes;

namespace Business.Interfaces.Services
{
    public interface  INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Adicionar(Notificacao notificacao);
    }
}
