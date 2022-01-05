using System.Text.Json;

namespace AspelTools.Server.Licencia
{
    public class RepositorioLicencia: IRepositorioLicencia
    {
        private readonly string rutaLicencia;

        public RepositorioLicencia()
        {
            rutaLicencia = Path.Combine(Environment.CurrentDirectory, "Licencia.json");
        }

        public void ActualizaFechaUltimaValidacion()
        {
            var licencia = File.ReadAllText(rutaLicencia);
            var conversion = JsonSerializer.Deserialize<Licencia>(licencia);
            conversion.FechaUltimaValidacion = DateTime.Now;
            var opciones = new JsonSerializerOptions();
            opciones.WriteIndented = true;
            File.WriteAllText(rutaLicencia, JsonSerializer.Serialize(conversion, opciones));
        }

        public void GuardaIdentificadorLicencia(string identificador)
        {
            var licencia = File.ReadAllText(rutaLicencia);
            var conversion = JsonSerializer.Deserialize<Licencia>(licencia);
            conversion.IdLicencia = identificador;
            var opciones = new JsonSerializerOptions();
            opciones.WriteIndented = true;
            File.WriteAllText(rutaLicencia, JsonSerializer.Serialize(conversion, opciones));
        }

        public string ObtieneIdentificadorAplicacipn()
        {
            var licencia = File.ReadAllText(rutaLicencia);
            return JsonSerializer.Deserialize<Licencia>(licencia)?.IdAplicacion ?? "";
        }

        public string ObtieneIdentificadorCliente()
        {
            var licencia = File.ReadAllText(rutaLicencia);
            return JsonSerializer.Deserialize<Licencia>(licencia)?.IdCliente ?? "";
        }

        public string ObtieneIdentificadorLicencia()
        {
            var licencia = File.ReadAllText(rutaLicencia);
            return JsonSerializer.Deserialize<Licencia>(licencia)?.IdLicencia ?? "";
        }

        public bool SeDebeValidarLicencia()
        {
            var licencia = File.ReadAllText(rutaLicencia);
            var json = JsonSerializer.Deserialize<Licencia>(licencia);
            if (DateTime.Now > json.FechaUltimaValidacion.AddDays(2))
            {
                return true;
            }
            return false;
        }
    }
}
