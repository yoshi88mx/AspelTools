using AspelTools.Server.Configuracion;
using AspelTools.Server.Modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace AspelTools.Server.Servicios
{
    public class ServicioMultialmacen : IServicioMultialmacen
    {
        private readonly AspelSAEContext _context;
        private readonly string _numeroEmpresa;

        public ServicioMultialmacen(AspelSAEContext context, IOptions<ConfiguracionAspelSAE> configAspel)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _numeroEmpresa = configAspel.Value.NumeroEmpresa.ToString("D2");
        }

        public async Task AltaClaveEnAlmacenDestino(string idAlmacenDestino, string claveProducto)
        {
            await _context.Multialmacen.AddAsync(new MultiAlmacen { ClaveArticulo = claveProducto, ClaveAlmacen = Convert.ToInt32(idAlmacenDestino) });
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ClaveExisteEnAlmacenDestino(string idAlmacendestino, string claveProducto)
        {
            return await _context.Multialmacen.AnyAsync(t => t.ClaveAlmacen == Convert.ToInt32(idAlmacendestino) & t.ClaveArticulo == claveProducto);
        }

        public Task GeneraTraspaso(string idAlmacenOrigen, string idAlmacenDestino, double cantidad, string claveProducto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Articulo>> ObtienePorAlmacenPorLineaConExistencia(string idAlmacen, string linea)
        {
            var querry = $"SELECT M.CVE_ART, A.DESCR as ALMACEN, I.DESCR, I.LIN_PROD, M.EXIST FROM MULT{_numeroEmpresa} M LEFT JOIN INVE{_numeroEmpresa} I ON I.CVE_ART = M.CVE_ART LEFT JOIN ALMACENES{_numeroEmpresa} A ON A.CVE_ALM = M.CVE_ALM WHERE M.CVE_ALM = {idAlmacen} AND M.EXIST > 0 AND I.LIN_PROD LIKE '{linea}'";

            return await _context.Articulos
                .FromSqlRaw(querry)
                .ToListAsync();
        }


    }
}
