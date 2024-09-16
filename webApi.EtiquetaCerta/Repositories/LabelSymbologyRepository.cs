using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Contexts;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Interfaces;

namespace webApi.EtiquetaCerta.Repositories
{
    public class LabelSymbologyRepository : ILabelSymbologyRepository
    {
        private readonly EtiquetaCertaContext _context;

        public LabelSymbologyRepository(EtiquetaCertaContext context)
        {
            _context = context;
        }

        // Adiciona uma nova relação LabelSymbology
        public async Task AddAsync(LabelSymbology labelSymbology)
        {
            await _context.LabelSymbologies.AddAsync(labelSymbology);
            await _context.SaveChangesAsync();
        }

        // Remove todas as simbologias associadas a uma etiqueta
        public async Task RemoveByLabelIdAsync(Guid labelId)
        {
            var symbologies = await _context.LabelSymbologies
                .Where(ls => ls.IdLabel == labelId)
                .ToListAsync();

            _context.LabelSymbologies.RemoveRange(symbologies);
            await _context.SaveChangesAsync();
        }

    }
}
