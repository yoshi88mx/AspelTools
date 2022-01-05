using System.Net.NetworkInformation;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace AspelTools.Server.Licencia
{
    public class ServicioLicencia: IServicioLicencia
    {
        private readonly IRepositorioLicencia _repositorioLicencia;
        private readonly HttpClient _http;

        public ServicioLicencia(IRepositorioLicencia repositorioLicencia, HttpClient http)
        {
            _repositorioLicencia = repositorioLicencia ?? throw new ArgumentNullException(nameof(repositorioLicencia));
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }
        public async Task<bool> LicenciaEsValida()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            if (_repositorioLicencia.SeDebeValidarLicencia())
            {
                var nic = NetworkInterface.GetAllNetworkInterfaces();
                var licenciaDeJSON = _repositorioLicencia.ObtieneIdentificadorLicencia();
                var idAplicacion = _repositorioLicencia.ObtieneIdentificadorAplicacipn();
                if (string.IsNullOrEmpty(licenciaDeJSON))
                {
                    var macaddress = nic[0].GetPhysicalAddress();
                    var respuestaLicenciaPorMAC = await _http.GetAsync($"api/v1/licencias/obtienepormacaddress/{idAplicacion}/{macaddress}");
                    if (respuestaLicenciaPorMAC.IsSuccessStatusCode)
                    {
                        var licenciapormac = await respuestaLicenciaPorMAC.Content.ReadFromJsonAsync<LicenciaDTO>();
                        _repositorioLicencia.GuardaIdentificadorLicencia(licenciapormac.Id);
                        if (licenciapormac.EsActiva)
                        {
                            _repositorioLicencia.ActualizaFechaUltimaValidacion();
                        }
                        return licenciapormac.EsActiva;
                    }
                    var aplicacion = await _http.GetFromJsonAsync<Aplicacion>($"api/v1/aplicaciones/{idAplicacion}");
                    if (aplicacion is null)
                    {
                        return false;
                    }
                    var licenciaBody = new LicenciaDTO
                    {
                        IdAplicacion = _repositorioLicencia.ObtieneIdentificadorAplicacipn(),
                        IdCliente = _repositorioLicencia.ObtieneIdentificadorCliente(),
                        Equipo = new Equipo
                        {
                            MACAddress = macaddress.ToString(),
                            Nombre = Environment.MachineName,
                            SO = Environment.OSVersion.VersionString,
                            Usuario = Environment.UserName
                        }
                    };
                    var respuestaGeneraLicencia = await _http.PostAsJsonAsync("api/v1/licencias/", licenciaBody);
                    var licenciaNueva = await respuestaGeneraLicencia.Content.ReadFromJsonAsync<LicenciaDTO>();
                    if (respuestaGeneraLicencia == null || respuestaGeneraLicencia.StatusCode != System.Net.HttpStatusCode.OK || licenciaNueva is null)
                    {
                        return false;
                    }
                    _repositorioLicencia.GuardaIdentificadorLicencia(licenciaNueva.Id);
                    return licenciaNueva.EsActiva;
                }
                var respuestaValidacion = await _http.GetFromJsonAsync<LicenciaDTO>($"api/v1/licencias/{licenciaDeJSON}");
                if (respuestaValidacion is not null)
                {
                    if (respuestaValidacion.EsActiva)
                    {
                        _repositorioLicencia.ActualizaFechaUltimaValidacion();
                    }
                    return respuestaValidacion.EsActiva;
                }
                return false;

            }
            return true;
        }
    }

    public class LicenciaDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("esactiva")]
        public bool EsActiva { get; set; }

        [JsonPropertyName("idcliente")]
        public string IdCliente { get; set; }

        [JsonPropertyName("idaplicacion")]
        public string IdAplicacion { get; set; }

        [JsonPropertyName("equipo")]
        public Equipo Equipo { get; set; }

        [JsonPropertyName("creacion")]
        public DateTime Creacion { get; set; }

        [JsonPropertyName("ultimavalidacion")]
        public DateTime UltimaValidacion { get; set; }
    }

    public class Equipo
    {
        [JsonPropertyName("macaddress")]
        public string MACAddress { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("usuario")]
        public string Usuario { get; set; }

        [JsonPropertyName("so")]
        public string SO { get; set; }
    }

    public class Aplicacion
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("fechacreacion")]
        public DateTime FechaCreacion { get; set; }

        [JsonPropertyName("pruebaendias")]
        public int PruebaEnDias { get; set; }
    }
}
