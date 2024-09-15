using webApi.EtiquetaCerta.Domains;
using System;
using System.Threading.Tasks;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface ISymbologyTranslateRepository
    {
        Task AddAsync(SymbologyTranslate symbologyTranslate);
    }
}
