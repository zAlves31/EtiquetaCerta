using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Contexts;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Interfaces;

namespace webApi.EtiquetaCerta.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        private readonly EtiquetaCertaContext _context;

        public LabelRepository(EtiquetaCertaContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Label label)
        {
            _context.Labels.Add(label);
            await _context.SaveChangesAsync();
        }

        public async Task<Legislation> GetByIdAsync(Guid id)
        {
            return await _context.Legislations
                .FirstOrDefaultAsync(l => l.Id == id);
        }

    }
}
