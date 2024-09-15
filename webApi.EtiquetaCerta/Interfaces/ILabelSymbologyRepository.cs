
using webApi.EtiquetaCerta.Domains;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface ILabelSymbologyRepository
    {
        Task AddAsync(LabelSymbology labelSymbology);
        Task RemoveByLabelIdAsync(Guid labelId);
        Task<List<LabelSymbology>> GetByLabelIdAsync(Guid labelId);
    }
}
