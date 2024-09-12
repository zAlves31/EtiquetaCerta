using webApi.EtiquetaCerta.Domains;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface ILegislationRepository
    {
        Task AddLegislationAsync(Legislation legislation);
        Task<ConservationProcess> GetConservationProcessByIdAsync(Guid processId);
        Task<Symbology> GetSymbologyByIdAsync(Guid symbologyId);

        public List<Legislation> List();

        public void Update(Legislation legislation);

        public void Delete(Guid id);


    }
}
