using webApi.EtiquetaCerta.Domains;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface ILegislationRepository
    {

        Task<ConservationProcess?> GetByIdAsync(Guid id);

        Task AddAsync(Legislation legislation);

        public List<Legislation> List();

        public void Update(Legislation legislation);

        public void Delete(Guid id);


    }
}
