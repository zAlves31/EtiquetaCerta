using Microsoft.EntityFrameworkCore;
using webApi.EtiquetaCerta.Contexts;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Interfaces;

namespace webApi.EtiquetaCerta.Repositories
{
    public class SymbologyTranslateRepository : ISymbologyTranslateRepository
    {
        private readonly EtiquetaCertaContext _context;

        public SymbologyTranslateRepository(EtiquetaCertaContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SymbologyTranslate symbologyTranslate)
        {
            await _context.SymbologyTranslates.AddAsync(symbologyTranslate);
            await _context.SaveChangesAsync();
        }
    }
}
