using AspelTools.Server.Helpers;
using AspelTools.Server.Modelo;

namespace AspelTools.Server.Servicios
{
    public class ServicioMovimientosInventario : IServicioMovimientosInventario
    {
        private readonly AspelSAEContext _context;

        public ServicioMovimientosInventario(AspelSAEContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AgregaMovimientoTraspaso(MovInvTras mov)
        {
            var movimiento = ObtieneEntidad(mov);
            await _context.MovimientosInventario.AddAsync(movimiento);
            await _context.SaveChangesAsync();
        }

        private MovimientoInventario ObtieneEntidad(MovInvTras entrada)
        {
            return new MovimientoInventario
            {
                CVE_ART = entrada.ClaveProducto,
                ALMACEN = entrada.Almacen,
                NUM_MOV = entrada.NumeroMovimiento,
                CVE_CPTO = entrada.Concepto,
                FECHA_DOCU = DateTime.Now.Date,
                TIPO_DOC = "M",
                REFER = entrada.Referencia,
                CANT = entrada.ExistenciaPrevia,
                COSTO = entrada.Costo,
                EXIST_G = entrada.ExistenciaGlobal,
                EXISTENCIA = entrada.ExistenciaNueva,
                TIPO_PROD = "P",
                FACTOR_CON = 1,
                FECHAELAB = DateTime.Now.Date,
                CVE_FOLIO = $"{entrada.Folio}",
                SIGNO = entrada.Signo,
                COSTEADO = "S",
                COSTO_PROM_INI = entrada.Costo,
                COSTO_PROM_FIN = entrada.Costo,
                COSTO_PROM_GRAL = entrada.Costo,
                DESDE_INVE = "S",
                MOV_ENLAZADO = entrada.NumeroMovimientoEnlazado
            };
        }
    }
}
