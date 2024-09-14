using System;
using System.Threading.Tasks;
using webApi.EtiquetaCerta.Domains;

namespace webApi.EtiquetaCerta.Interfaces
{
    public interface IProcessRepository
    {
        Task<ConservationProcess?> GetByIdAsync(Guid id);
    }
}
