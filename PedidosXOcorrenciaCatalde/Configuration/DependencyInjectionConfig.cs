using Business.Interfaces.Repositorys;
using Business.Interfaces.Services;
using Business.Notificacoes;
using Business.Services;
using Data.Context;
using Data.Repository;

namespace Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencias(this IServiceCollection services)
        {
            services.AddScoped<ApiDbContext>();
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();

            services.AddScoped<IOcorrenciaService, OcorrenciaService>();
            services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();
            return services;
        }
    }
}
