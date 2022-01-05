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
        private readonly IServicioControl _servicioControl;
        private readonly IServicioInventario _servicioInventario;
        private readonly IServicioMovimientosInventario _servicioMovimientosInventario;
        private readonly ILogger<MultialmacenController> _logger;

        public MultialmacenController(IServicioMultialmacen servicioMultialmacen,
                                      IServicioControl servicioControl,
                                      IServicioInventario servicioInventario,
                                      IServicioMovimientosInventario servicioMovimientosInventario,
                                      ILogger<MultialmacenController> logger)
        {
            _servicioMultialmacen = servicioMultialmacen ?? throw new ArgumentNullException(nameof(servicioMultialmacen));
            _servicioControl = servicioControl ?? throw new ArgumentNullException(nameof(servicioControl));
            _servicioInventario = servicioInventario ?? throw new ArgumentNullException(nameof(servicioInventario));
            _servicioMovimientosInventario = servicioMovimientosInventario ?? throw new ArgumentNullException(nameof(servicioMovimientosInventario));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("reporte")]
        public async Task<ActionResult> GeneraReporte([FromQuery] string almacen, [FromQuery] string linea)
        {
            _logger.LogInformation(nameof(GeneraReporte));
            var archivo = GenerateCSV(await _servicioMultialmacen.ObtieneProductosPorAlmacenPorLineaConExistencia(almacen, linea));
            var stream = new FileStream(archivo, FileMode.Open);
            return File(stream, "application/octet-stream", $"ReporteArticulosPorTraspasar-{Guid.NewGuid()}.csv");

        }

        [HttpPost]
        [Route("traspasoporlinea")]
        public async Task<ActionResult> GeneraTraspasoPorLinea([FromQuery] string almacenOrigen, [FromQuery] string almacenDestino, [FromQuery] string linea, [FromQuery] string referencia)
        {
            _logger.LogInformation(nameof(GeneraTraspasoPorLinea));
            var productos = await _servicioMultialmacen.ObtieneProductosPorAlmacenPorLineaConExistencia(almacenOrigen, linea);

            if (productos.Any())
            {
                var ultimoFolio = await _servicioControl.ObtieneUltimoFolio();
                var ultimoMovimiento = await _servicioControl.ObtieneUltimoNumeroMovimiento();
                foreach (var producto in productos)
                {
                    _logger.LogInformation($"Producto: {producto.Descripcion}");
                    if (!await _servicioMultialmacen.ClaveExisteEnAlmacenDestino(almacenDestino, producto.Clave))
                    {
                        _logger.LogInformation($"Alta de clave en almacen destino: Almacen:{almacenDestino} Producto:{producto.Clave}");
                        await _servicioMultialmacen.AltaClaveEnAlmacenDestino(almacenDestino, producto.Clave);
                    }

                    var productoEnDestino = await _servicioMultialmacen.Obtiene(almacenDestino, producto.Clave);
                    var nuevaExistencia = productoEnDestino.Existencia + producto.Existencia;
                    productoEnDestino.Existencia = nuevaExistencia;
                    _logger.LogInformation($"Actualiza existencias en destino: {productoEnDestino.Existencia}");
                    await _servicioMultialmacen.ActualizaExistencia(productoEnDestino);

                    var existenciaGlobal = await _servicioMultialmacen.ObtieneExistenciaGlobal(producto.Clave);
                    var articuloEnInventario = await _servicioInventario.ObtieneArticuloPorClave(producto.Clave);

                    ultimoMovimiento++;
                    ultimoFolio++;

                    _logger.LogInformation(nameof(_servicioMovimientosInventario.AgregaMovimientoTraspaso));
                    await _servicioMovimientosInventario.AgregaMovimientoTraspaso(new Helpers.MovInvTras 
                    { 
                        ClaveProducto = producto.Clave,
                        NumeroMovimiento = ultimoMovimiento,
                        Almacen = Convert.ToInt32(almacenOrigen),
                        Costo = articuloEnInventario.ULT_COSTO.Value,
                        ExistenciaGlobal = existenciaGlobal,
                        ExistenciaNueva = 0,
                        ExistenciaPrevia = producto.Existencia,
                        NumeroMovimientoEnlazado = 0,
                        Folio = ultimoFolio,
                        Signo = -1,
                        Referencia = referencia, 
                        Concepto = 58
                    });

                    var numeroMovimientoAnterior = ultimoMovimiento;

                    ultimoMovimiento++;

                    _logger.LogInformation(nameof(_servicioMovimientosInventario.AgregaMovimientoTraspaso));
                    await _servicioMovimientosInventario.AgregaMovimientoTraspaso(new Helpers.MovInvTras
                    {
                        ClaveProducto = producto.Clave,
                        NumeroMovimiento = ultimoMovimiento,
                        Almacen = Convert.ToInt32(almacenDestino),
                        Costo = articuloEnInventario.ULT_COSTO.Value,
                        ExistenciaGlobal = existenciaGlobal,
                        ExistenciaNueva = nuevaExistencia,
                        ExistenciaPrevia = producto.Existencia,
                        NumeroMovimientoEnlazado = numeroMovimientoAnterior,
                        Folio = ultimoFolio,
                        Signo = 1,
                        Referencia = referencia,
                        Concepto = 7
                    });

                    var productoEnOrigen = await _servicioMultialmacen.Obtiene(almacenOrigen, producto.Clave);
                    _logger.LogInformation($"Actualiza existencias en origen: {productoEnOrigen.Existencia}");
                    productoEnOrigen.Existencia = 0;
                    await _servicioMultialmacen.ActualizaExistencia(productoEnDestino);
                }

                _logger.LogInformation($"Actualiza en tabla de control: Folio:{ultimoFolio}, NumeroMovimiento:{ultimoMovimiento}");
                await _servicioControl.GuardaUltimoFolio(ultimoFolio);
                await _servicioControl.GuardaUltimoMovimiento(ultimoMovimiento);
            }
            
            return Ok();
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
