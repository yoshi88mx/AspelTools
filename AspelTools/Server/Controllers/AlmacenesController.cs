using AspelTools.Server.Servicios;
using AspelTools.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AspelTools.Server.Controllers
{
    [ApiController]
    [Route("api/v1/almacenes")]
    public class AlmacenesController : ControllerBase
    {
        private readonly IServicioAlmacenes _servicioAlmacenes;
        private readonly ILogger<AlmacenesController> _logger;

        public AlmacenesController(IServicioAlmacenes servicioAlmacenes, ILogger<AlmacenesController> logger)
        {
            _servicioAlmacenes = servicioAlmacenes ?? throw new ArgumentNullException(nameof(servicioAlmacenes));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<List<AlmacenDTO>>> ObtieneAlmacenesAsync()
        {
            _logger.LogInformation(nameof(ObtieneAlmacenesAsync));
            return await _servicioAlmacenes.GetAllAsync();
        }
    }
}