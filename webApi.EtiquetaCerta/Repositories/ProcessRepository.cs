using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Contexts;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Interfaces;

namespace webApi.EtiquetaCerta.Repositories
{
    public class ProcessRepository : IProcessRepository
    {
        private readonly EtiquetaCertaContext _context;

        public ProcessRepository(EtiquetaCertaContext context)
        {
            _context = context;
        }

        public async Task<ConservationProcess?> GetByIdAsync(Guid id)
        {
            return await _context.ConservationProcesses
                .Include(p => p.Symbologies)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
