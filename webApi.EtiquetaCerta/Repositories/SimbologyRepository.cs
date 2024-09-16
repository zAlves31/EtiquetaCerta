using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Contexts;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Interfaces;

namespace webApi.EtiquetaCerta.Repositories
{
    public class SymbologyRepository : ISymbologyRepository
    {
        private readonly EtiquetaCertaContext _context;

        public SymbologyRepository(EtiquetaCertaContext context)
        {
            _context = context;
        }

        public async Task<Symbology?> GetByIdAsync(Guid id)
        {
            return await _context.Symbologies
                .Include(s => s.IdProcessNavigation) 
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
