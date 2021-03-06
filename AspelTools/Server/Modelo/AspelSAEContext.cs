using AspelTools.Server.Configuracion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AspelTools.Server.Modelo
{
    public class AspelSAEContext: DbContext
    {
        private readonly ConfiguracionAspelSAE configAspel;

        public AspelSAEContext(DbContextOptions<AspelSAEContext> options, IOptions<ConfiguracionAspelSAE> configAspel) : base(options)
        {
            this.configAspel = configAspel.Value ?? throw new ArgumentNullException(nameof(configAspel));
        }

        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Control> Controles { get; set; }
        public DbSet<MultiAlmacen> Multialmacen { get; set; }
        public DbSet<Articulo> Inventario { get; set; }
        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Almacen
            modelBuilder.Entity<Almacen>().HasKey(t => t.Clave);

            modelBuilder.Entity<Almacen>().ToTable($"ALMACENES{configAspel.NumeroEmpresa.ToString("D2")}");

            modelBuilder.Entity<Almacen>()
                .Property(t => t.Clave)
                .HasColumnName("CVE_ALM");

            modelBuilder.Entity<Almacen>()
                .Property(t => t.Descripcion)
                .HasColumnName("DESCR");

            modelBuilder.Entity<Producto>().HasKey(t => t.Clave);


            //Articulo
            modelBuilder.Entity<Producto>()
                .Property(t => t.Clave)
                .HasColumnName("CVE_ART");

            modelBuilder.Entity<Producto>()
                .Property(t => t.Almacen)
                .HasColumnName("ALMACEN");

            modelBuilder.Entity<Producto>()
                .Property(t => t.Descripcion)
                .HasColumnName("DESCR");

            modelBuilder.Entity<Producto>()
                .Property(t => t.Linea)
                .HasColumnName("LIN_PROD");

            modelBuilder.Entity<Producto>()
                .Property(t => t.Existencia)
                .HasColumnName("EXIST");

            //Control
            modelBuilder.Entity<Control>().ToTable($"TBLCONTROL{configAspel.NumeroEmpresa.ToString("D2")}");

            modelBuilder.Entity<Control>().HasKey(t => t.IdTabla);

            modelBuilder.Entity<Control>()
                .Property(t => t.IdTabla)
                .HasColumnName("ID_TABLA");

            modelBuilder.Entity<Control>()
                .Property(t => t.UltimaClave)
                .HasColumnName("ULT_CVE");

            //MultiAlmacen
            modelBuilder.Entity<MultiAlmacen>().ToTable($"MULT{configAspel.NumeroEmpresa.ToString("D2")}");

            modelBuilder.Entity<MultiAlmacen>().HasKey(t => new { t.ClaveArticulo, t.ClaveAlmacen });

            modelBuilder.Entity<MultiAlmacen>()
                .Property(t => t.ClaveArticulo)
                .HasColumnName("CVE_ART");

            modelBuilder.Entity<MultiAlmacen>()
                .Property(t => t.ClaveAlmacen)
                .HasColumnName("CVE_ALM");

            modelBuilder.Entity<MultiAlmacen>()
                .Property(t => t.Status)
                .HasColumnName("STATUS");

            modelBuilder.Entity<MultiAlmacen>()
                .Property(t => t.ControlAlmacen)
                .HasColumnName("CTRL_ALM");

            modelBuilder.Entity<MultiAlmacen>()
                .Property(t => t.Existencia)
                .HasColumnName("EXIST");

            modelBuilder.Entity<MultiAlmacen>()
                .Property(t => t.StockMinimo)
                .HasColumnName("STOCK_MIN");

            modelBuilder.Entity<MultiAlmacen>()
                .Property(t => t.StockMaximo)
                .HasColumnName("STOCK_MAX");

            modelBuilder.Entity<MultiAlmacen>()
                .Property(t => t.ComprasPorRecibir)
                .HasColumnName("COMP_X_REC");

            modelBuilder.Entity<MultiAlmacen>()
                .Property(t => t.UUID)
                .HasColumnName("UUID");

            modelBuilder.Entity<MultiAlmacen>()
                .Property(t => t.VersionSincronizacion)
                .HasColumnName("VERSION_SINC");

            //Inventario
            modelBuilder.Entity<Articulo>().ToTable($"INVE{configAspel.NumeroEmpresa.ToString("D2")}");
            modelBuilder.Entity<Articulo>().HasKey(t => t.CVE_ART);

            //Movimiento al Inventario
            modelBuilder.Entity<MovimientoInventario>().ToTable($"MINVE{configAspel.NumeroEmpresa.ToString("D2")}");
            modelBuilder.Entity<MovimientoInventario>().HasKey(t => new { t.CVE_ART, t.ALMACEN, t.NUM_MOV, t.CVE_CPTO });

        }
    }
}
