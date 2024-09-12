using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Interfaces;

namespace webApi.EtiquetaCerta.Repositories
{
    public class LegislationRepository : ILegislationRepository
    {
        private readonly DbContext _context;

        public LegislationRepository(DbContext context)
        {
            _context = context;
        }

        public List<Legislation> List()
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Legislation legislation)
        {
            throw new NotImplementedException();
        }

        public async Task AddLegislationAsync(Legislation legislation)
        {
            _context.Add(legislation);
            await _context.SaveChangesAsync();
        }

        public async Task<ConservationProcess> GetConservationProcessByIdAsync(Guid processId)
        {
            return await _context.Set<ConservationProcess>()
                .Include(p => p.Symbologies)
                .FirstOrDefaultAsync(p => p.Id == processId);
        }

        public async Task<Symbology> GetSymbologyByIdAsync(Guid symbologyId)
        {
            return await _context.Set<Symbology>()
                .FirstOrDefaultAsync(s => s.Id == symbologyId);
        }
    }
}
