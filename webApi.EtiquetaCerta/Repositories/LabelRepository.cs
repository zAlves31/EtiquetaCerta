using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Contexts;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Dtos;
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

        public async Task<Legislation?> GetByIdAsync(Guid id)
        {
            return await _context.Legislations
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Label?> GetLabelWithLegislationByIdAsync(Guid id)
        {
            return await _context.Labels
                .Include(l => l.IdLegislationNavigation)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<LabelDto>> GetAllLabelsWithLegislationAndSymbologyAsync()
        {
            var labels = await _context.Labels
                .Include(l => l.IdLegislationNavigation)
                .Select(l => new LabelDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    IdLegislation = l.IdLegislation ?? Guid.Empty,
                    SelectedSymbology = _context.LabelSymbologies
                        .Where(ls => ls.IdLabel == l.Id)
                        .Join(_context.Symbologies,
                              ls => ls.IdSymbology,
                              s => s.Id,
                              (ls, s) => new SymbologyGetDto // Usando SymbologyDto
                              {
                                  Id = s.Id,
                                  IdProcess = s.IdProcess,
                                  Translate = s.Name
                              })
                        .ToList() // Corrigido para usar SymbologyDto
                })
                .ToListAsync();

            return labels;
        }
    }
}
