using AspelTools.Server.Servicios;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AspelTools.Server.Controllers
{
    [ApiController]
    [Route("api/v1/multialmacen")]
    public class MultialmacenController : ControllerBase
    {
        private readonly IServicioMultialmacen _servicioMultialmacen;

        public MultialmacenController(IServicioMultialmacen servicioMultialmacen)
        {
            _servicioMultialmacen = servicioMultialmacen ?? throw new ArgumentNullException(nameof(servicioMultialmacen));
        }

        [HttpGet]
        [Route("reporte")]
        public async Task<ActionResult> GeneraReporte([FromQuery] string almacen, [FromQuery] string linea)
        {
            var archivo = GenerateCSV(await _servicioMultialmacen.ObtienePorAlmacenPorLineaConExistencia(almacen, linea));
            var stream = new FileStream(archivo, FileMode.Open);
            return File(stream, "application/octet-stream", $"ReporteArticulosPorTraspasar-{Guid.NewGuid()}.csv");

        }

        private string GenerateCSV<T>(List<T> list)
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\files\\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\files\\");
            }
            var archivo = Environment.CurrentDirectory + "\\files\\reporte" + Guid.NewGuid().ToString() + ".csv";
            using (var writer = new StreamWriter(archivo))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(list);
            }
            return archivo;
        }
    }
}
