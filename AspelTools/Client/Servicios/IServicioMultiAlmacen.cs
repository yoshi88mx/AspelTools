namespace AspelTools.Client.Servicios
{
    public interface IServicioMultiAlmacen
    {
        Task<HttpResponseMessage> GeneraTraspasoPorLinea(string almacenOrigen, string almacenDestino, string linea, string referencia);
    }
}
