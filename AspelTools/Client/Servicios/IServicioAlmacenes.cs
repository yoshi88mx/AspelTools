using AspelTools.Shared;

namespace AspelTools.Client.Servicios
{
    public interface IServicioAlmacenes
    {
        Task<List<AlmacenDTO>> GetAllAsync();
    }
}
