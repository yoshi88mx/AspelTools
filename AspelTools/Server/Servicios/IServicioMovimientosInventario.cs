using AspelTools.Server.Helpers;
using AspelTools.Server.Modelo;

namespace AspelTools.Server.Servicios
{
    public interface IServicioMovimientosInventario
    {
        Task AgregaMovimientoTraspaso(MovInvTras mov);
    }
}
