using webApi.EtiquetaCerta.Domains;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface ISymbologyRepository
    {
        Task<Symbology?> GetByIdAsync(Guid id);

    }
}
