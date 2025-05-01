using AutoMapper;
using Business.Models;
using Business.Models.Dtos;

namespace Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Pedido, PedidoDTO>();


            CreateMap<Ocorrencia, OcorrenciaDTO>()
                .ForMember(dest => dest.Pedido, opt => opt.Ignore());
        }
    }
}
