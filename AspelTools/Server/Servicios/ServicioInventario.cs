using AspelTools.Server.Modelo;
using Microsoft.EntityFrameworkCore;

namespace AspelTools.Server.Servicios
{
    public class ServicioInventario : IServicioInventario
    {
        private readonly AspelSAEContext _context;

        public ServicioInventario(AspelSAEContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Articulo> ObtieneArticuloPorClave(string clave)
        {
            //TODO: Aqui llega nulo
            return await _context.Inventario.FirstOrDefaultAsync(t => t.CVE_ART == clave);
        }
    }
}
