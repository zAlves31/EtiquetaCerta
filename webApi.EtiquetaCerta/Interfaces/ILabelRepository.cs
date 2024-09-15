using webApi.EtiquetaCerta.Domains;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface ILabelRepository
    {
        Task AddAsync(Label label);

        Task<Legislation?> GetByIdAsync(Guid id);
    }
}
