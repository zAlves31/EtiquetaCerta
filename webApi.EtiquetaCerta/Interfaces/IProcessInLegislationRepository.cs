using webApi.EtiquetaCerta.Domains;
using System;
using System.Threading.Tasks;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface IProcessInLegislationRepository
    {
        Task AddAsync(ProcessInLegislation processInLegislation);

        Task DeleteByLegislationIdAsync(Guid legislationId);

        Task<IEnumerable<ProcessInLegislation>> GetByLegislationIdAsync(Guid legislationId); 
    }
}
