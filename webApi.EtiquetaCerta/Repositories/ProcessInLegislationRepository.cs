using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Contexts;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Interfaces;

namespace webApi.EtiquetaCerta.Repositories
{
    public class ProcessInLegislationRepository : IProcessInLegislationRepository
    {
        private readonly EtiquetaCertaContext _context;

        public ProcessInLegislationRepository(EtiquetaCertaContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProcessInLegislation processInLegislation)
        {
            await _context.ProcessInLegislations.AddAsync(processInLegislation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProcessInLegislation>> GetByLegislationIdAsync(Guid legislationId)
        {
            return await _context.Set<ProcessInLegislation>()
                .Where(p => p.IdLegislation == legislationId)
                .ToListAsync();
        }
    }
}
