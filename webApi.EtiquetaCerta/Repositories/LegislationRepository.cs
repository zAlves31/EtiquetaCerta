using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Contexts;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Interfaces;

namespace webApi.EtiquetaCerta.Repositories
{
    public class LegislationRepository : ILegislationRepository
    {
        private readonly EtiquetaCertaContext _context; // Use o contexto específico

        public LegislationRepository(EtiquetaCertaContext context)
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

        public async Task<ConservationProcess> GetByIdAsync(Guid id)
        {
            return await _context.ConservationProcesses
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Legislation legislation)
        {
            _context.Legislations.Add(legislation); // Adicionando a legislação
            await _context.SaveChangesAsync();
        }
    }
}
