using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webApi.EtiquetaCerta.Domains;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface IConservationProcessRepository
    {
        Task<ConservationProcess> GetByIdAsync(Guid id);
        Task<List<ConservationProcess>> ListAsync();
        Task AddAsync(ConservationProcess conservationProcess);
        void Update(ConservationProcess conservationProcess);
        void Delete(Guid id);
    }
}
