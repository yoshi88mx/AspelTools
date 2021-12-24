using AspelTools.Server.Modelo;

namespace AspelTools.Server.Servicios
{
    public interface IServicioMultialmacen
    {
        Task<List<Producto>> ObtieneProductosPorAlmacenPorLineaConExistencia(string idAlmacen, string linea);
        Task GeneraTraspaso(string idAlmacenOrigen, string idAlmacenDestino, double cantidad, string claveProducto);
        Task<bool> ClaveExisteEnAlmacenDestino(string idAlmacendestino, string claveProducto);
        Task AltaClaveEnAlmacenDestino(string idAlmacenDestino, string claveProducto);
        Task<MultiAlmacen> Obtiene(string idAlmacen, string claveProducto);
        Task ActualizaExistencia(MultiAlmacen entrada);
        Task<double> ObtieneExistenciaGlobal(string claveProducto);

    }
}
