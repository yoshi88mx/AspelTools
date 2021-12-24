using AspelTools.Server.Configuracion;
using AspelTools.Server.Modelo;
using AspelTools.Server.Servicios;
using Microsoft.EntityFrameworkCore;

namespace AspelTools.Server
{
    public static class AspelToolsDependencyInjection
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfiguracionAspelSAE>(configuration.GetSection("AspelSAE"));

            services.AddDbContext<AspelSAEContext>(op => op.UseSqlServer(configuration.GetConnectionString("SQLSAE")));

            services.AddAutoMapper(typeof(Program));

            services.AddTransient<IServicioAlmacenes, ServicioAlmacenes>();
            services.AddTransient<IServicioMultialmacen, ServicioMultialmacen>();
            services.AddTransient<IServicioControl, ServicioControl>();
            services.AddTransient<IServicioInventario, ServicioInventario>();
            services.AddTransient<IServicioMovimientosInventario, ServicioMovimientosInventario>();

            return services;
        }
    }
}
