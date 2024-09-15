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

        public async Task<List<ConservationProcess>> ListAsync()
        {
            return await _context.ConservationProcesses
                .Include(cp => cp.ProcessInLegislations)
                .Include(cp => cp.Symbologies)
                .ToListAsync();
        }

        public async Task AddAsync(ConservationProcess conservationProcess)
        {
            await _context.ConservationProcesses.AddAsync(conservationProcess);
            await _context.SaveChangesAsync();
        }

        public void Update(ConservationProcess conservationProcess)
        {
            _context.ConservationProcesses.Update(conservationProcess);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var conservationProcess = _context.ConservationProcesses.Find(id);
            if (conservationProcess != null)
            {
                _context.ConservationProcesses.Remove(conservationProcess);
                _context.SaveChanges();
            }
        }
    }
}
