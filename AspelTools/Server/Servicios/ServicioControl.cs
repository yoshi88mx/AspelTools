using AspelTools.Server.Modelo;
using Microsoft.EntityFrameworkCore;

namespace AspelTools.Server.Servicios
{
    public class ServicioControl : IServicioControl
    {
        private readonly AspelSAEContext _context;

        public ServicioControl(AspelSAEContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task GuardaUltimoFolio(int ultimo)
        {
            var control = await _context.Controles.FirstOrDefaultAsync(t => t.IdTabla == 32);
            control.UltimaClave = ultimo;
            await _context.SaveChangesAsync();
        }

        public async Task GuardaUltimoMovimiento(int ultimo)
        {
            var control = await _context.Controles.FirstOrDefaultAsync(t => t.IdTabla == 44);
            control.UltimaClave = ultimo;
            await _context.SaveChangesAsync();
        }

        public async Task<int> ObtieneUltimoFolio()
        {
            var control = await _context.Controles.FirstOrDefaultAsync(t => t.IdTabla == 32);
            return control.UltimaClave;
        }

        public async Task<int> ObtieneUltimoNumeroMovimiento()
        {
            var control = await _context.Controles.FirstOrDefaultAsync(t => t.IdTabla == 44);
            return control.UltimaClave;
        }
    }
}
