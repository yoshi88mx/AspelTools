using AspelTools.Server.Modelo;

namespace AspelTools.Server.Servicios
{
    public interface IServicioInventario
    {
        Task<Articulo> ObtieneArticuloPorClave(string clave);
    }
}
