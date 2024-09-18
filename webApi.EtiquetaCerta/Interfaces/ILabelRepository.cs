using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Dtos;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface ILabelRepository
    {
        Task AddAsync(Label label);

        Task<Legislation?> GetByIdAsync(Guid id);

        Task<Label?> GetLabelWithLegislationByIdAsync(Guid id);

        Task<List<LabelDto>> GetAllLabelsWithLegislationAndSymbologyAsync(); // Adicione este método
    }
}
