using webApi.EtiquetaCerta.Domains;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface ILegislationRepository
    {
        Task<ConservationProcess?> GetByIdAsync(Guid id); 

        Task AddAsync(Legislation legislation);

        Task<List<Legislation>> ListAsync();

        void Update(Legislation legislation);

        Task DeleteAsync(Guid id);
    }
}
