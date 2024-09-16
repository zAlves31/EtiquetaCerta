using webApi.EtiquetaCerta.Domains;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface ILegislationRepository
    {

        Task AddAsync(Legislation legislation);

        Task<List<Legislation>> ListAsync();

    }
}
