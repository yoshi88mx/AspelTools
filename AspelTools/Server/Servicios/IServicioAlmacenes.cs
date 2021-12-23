using AspelTools.Shared;

namespace AspelTools.Server.Servicios
{
    public interface IServicioAlmacenes
    {
        Task<List<AlmacenDTO>> GetAllAsync();
    }
}
