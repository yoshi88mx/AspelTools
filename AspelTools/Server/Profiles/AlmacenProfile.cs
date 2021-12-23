using AspelTools.Server.Modelo;
using AspelTools.Shared;
using AutoMapper;

namespace AspelTools.Server.Profiles
{
    public class AlmacenProfile:Profile
    {
        public AlmacenProfile()
        {
            CreateMap<Almacen, AlmacenDTO>();
        }
    }
}
