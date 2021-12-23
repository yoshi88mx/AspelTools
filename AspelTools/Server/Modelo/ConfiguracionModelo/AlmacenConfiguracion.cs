using AspelTools.Server.Configuracion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;

namespace AspelTools.Server.Modelo.ConfiguracionModelo
{
    public class AlmacenConfiguracion : IEntityTypeConfiguration<Almacen>
    {
        private readonly ConfiguracionAspelSAE _options;

        public AlmacenConfiguracion(IOptions<ConfiguracionAspelSAE> options)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }
        public void Configure(EntityTypeBuilder<Almacen> builder)
        {
            builder.HasKey(t => t.Clave);

            builder.ToTable($"ALMACENES{_options.NumeroEmpresa.ToString("D2")}");

            builder
                .Property(t => t.Clave)
                .HasColumnName("CVE_ALM");

            builder
                .Property(t => t.Descripcion)
                .HasColumnName("DESCR");

        }
    }
}
