using webApi.EtiquetaCerta.Domains;
using System;
using System.Threading.Tasks;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface IProcessInLegislationRepository
    {
        Task AddAsync(ProcessInLegislation processInLegislation);

        Task<IEnumerable<ProcessInLegislation>> GetByLegislationIdAsync(Guid legislationId); 
    }
}
