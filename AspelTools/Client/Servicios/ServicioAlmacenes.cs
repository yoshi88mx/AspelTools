using AspelTools.Shared;
using System.Net.Http.Json;

namespace AspelTools.Client.Servicios
{
    public class ServicioAlmacenes : IServicioAlmacenes
    {
        private readonly HttpClient _http;
        private readonly string _endpoint = "api/v1/almacenes";

        public ServicioAlmacenes(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }
        public async Task<List<AlmacenDTO>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<AlmacenDTO>>(_endpoint) ?? new List<AlmacenDTO>();
        }
    }
}
