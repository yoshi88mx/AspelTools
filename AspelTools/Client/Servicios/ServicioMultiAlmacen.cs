namespace AspelTools.Client.Servicios
{
    public class ServicioMultiAlmacen : IServicioMultiAlmacen
    {
        private readonly HttpClient _http;
        private readonly string _url = "api/v1/multialmacen";

        public ServicioMultiAlmacen(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }
        public async Task<HttpResponseMessage> GeneraTraspasoPorLinea(string almacenOrigen, string almacenDestino, string linea, string referencia)
        {
            return await _http.PostAsync($"{_url}/traspasoporlinea?almacenOrigen={almacenOrigen}&almacenDestino={almacenDestino}&linea={linea}&referencia={referencia}",null);
        }
    }
}
