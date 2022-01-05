using AspelTools.Server.Licencia;

namespace AspelTools.Server.Middleware
{
    public class LicenciaMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServicioLicencia _licencia;
        public LicenciaMiddleware(RequestDelegate next, IServicioLicencia licencia)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _licencia = licencia ?? throw new ArgumentNullException(nameof(licencia));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var resultado = await _licencia.LicenciaEsValida();
            if (!resultado)
            {
                await httpContext.Response.WriteAsync("Ocurrio un error al validar la licencia, verifica tu conexion a internet o contacta con el desarrollador: yoshi88mx@hotmail.com");
                return;
            }
            await _next.Invoke(httpContext);
        }
    }
}
