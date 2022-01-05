namespace AspelTools.Server.Licencia
{
    public interface IRepositorioLicencia
    {
        void ActualizaFechaUltimaValidacion();
        void GuardaIdentificadorLicencia(string identificador);
        string ObtieneIdentificadorLicencia();
        string ObtieneIdentificadorAplicacipn();
        string ObtieneIdentificadorCliente();
        bool SeDebeValidarLicencia();
    }
}
