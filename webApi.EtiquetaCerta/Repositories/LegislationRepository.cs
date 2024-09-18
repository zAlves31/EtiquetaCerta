using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Contexts;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Interfaces;

namespace webApi.EtiquetaCerta.Repositories
{
    public class LegislationRepository : ILegislationRepository
    {
        private readonly EtiquetaCertaContext _context;

        public LegislationRepository(EtiquetaCertaContext context)
        {
            _context = context;
        }

        public async Task<List<Legislation>> ListAsync()
        {
            return await _context.Legislations
                .Include(l => l.ProcessInLegislations)
                    .ThenInclude(pil => pil.IdProcessNavigation) // Carregar o processo de conservação
                .Include(l => l.SymbologyTranslates)
                    .ThenInclude(st => st.IdSymbologyNavigation) // Carregar a simbologia
                .ToListAsync();
        }


        public async Task AddAsync(Legislation legislation)
        {
            await _context.Legislations.AddAsync(legislation);
            await _context.SaveChangesAsync();
        }


    }
}
