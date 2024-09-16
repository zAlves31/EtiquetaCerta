using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Contexts;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Interfaces;

namespace webApi.EtiquetaCerta.Repositories
{
    public class ConservationProcessRepository : IConservationProcessRepository
    {
        private readonly EtiquetaCertaContext _context;

        public ConservationProcessRepository(EtiquetaCertaContext context)
        {
            _context = context;
        }

        public async Task<ConservationProcess> GetByIdAsync(Guid id)
        {
            return await _context.ConservationProcesses
                .Include(cp => cp.ProcessInLegislations) // Include related ProcessInLegislations if needed
                .Include(cp => cp.Symbologies) // Include related Symbologies if needed
                .FirstOrDefaultAsync(cp => cp.Id == id);
        }
    }
}
