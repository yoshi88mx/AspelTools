using AspelTools.Server.Modelo;
using AspelTools.Shared;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AspelTools.Server.Servicios
{
    public class ServicioAlmacenes : IServicioAlmacenes
    {
        private readonly AspelSAEContext _context;
        private readonly IMapper _mapper;

        public ServicioAlmacenes(AspelSAEContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }
        public async Task<List<AlmacenDTO>> GetAllAsync()
        {
            return await _context.Almacenes.ProjectTo<AlmacenDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
