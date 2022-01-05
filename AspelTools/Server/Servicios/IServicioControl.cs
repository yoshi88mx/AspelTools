namespace AspelTools.Server.Servicios
{
    public interface IServicioControl
    {
        Task<int> ObtieneUltimoFolio();
        Task<int> ObtieneUltimoNumeroMovimiento();
        Task GuardaUltimoFolio(int ultimo);
        Task GuardaUltimoMovimiento(int ultimo);
    }
}
