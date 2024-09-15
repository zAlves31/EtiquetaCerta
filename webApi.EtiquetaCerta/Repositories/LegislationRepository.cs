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

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Legislations.FindAsync(id);
            if (entity != null)
            {
                _context.Legislations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }


        public void Update(Legislation legislation)
        {
            _context.Legislations.Update(legislation); // Atualiza a legislação
            _context.SaveChanges(); // Salva as alterações
        }

        public async Task<ConservationProcess> GetByIdAsync(Guid id)
        {
            return await _context.ConservationProcesses
                .FirstOrDefaultAsync(cp => cp.Id == id);
        }


        public async Task AddAsync(Legislation legislation)
        {
            await _context.Legislations.AddAsync(legislation);
            await _context.SaveChangesAsync();
        }
    }
}
