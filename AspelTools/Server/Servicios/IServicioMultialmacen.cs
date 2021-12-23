using AspelTools.Server.Modelo;

namespace AspelTools.Server.Servicios
{
    public interface IServicioMultialmacen
    {
        Task<List<Articulo>> ObtienePorAlmacenPorLineaConExistencia(string idAlmacen, string linea);
        Task GeneraTraspaso(string idAlmacenOrigen, string idAlmacenDestino, double cantidad, string claveProducto);
        Task<bool> ClaveExisteEnAlmacenDestino(string idAlmacendestino, string claveProducto);
        Task AltaClaveEnAlmacenDestino(string idAlmacenDestino, string claveProducto);
    }
}
