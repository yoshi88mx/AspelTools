namespace AspelTools.Server.Licencia
{
    public interface IServicioLicencia
    {
        Task<bool> LicenciaEsValida();
    }
}
