using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Dtos;
using webApi.EtiquetaCerta.Interfaces;

namespace webApi.EtiquetaCerta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegislationController : ControllerBase
    {
        private readonly ILegislationRepository _repository;

        public LegislationController(ILegislationRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLegislation([FromBody] LegislationPostDto request)
        {
            foreach (var process in request.ConservationProcesses)
            {
                var dbProcess = await _repository.GetConservationProcessByIdAsync(process.IdProcess);

                if (dbProcess == null)
                {
                    return BadRequest($"Process with ID {process.IdProcess} not found.");
                }

                foreach (var symbology in process.Symbologies)
                {
                    var dbSymbology = await _repository.GetSymbologyByIdAsync(symbology.Id);

                    if (dbSymbology == null)
                    {
                        return BadRequest($"Symbology with ID {symbology.Id} not found.");
                    }

                    if (dbSymbology.IdProcess != process.IdProcess)  // Correção aqui
                    {
                        return BadRequest($"Symbology {symbology.Translate} does not belong to process {dbProcess.Name}.");
                    }
                }
            }

            var legislation = new Legislation
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                OfficialLanguage = request.OfficialLanguage,
                ConservationProcesses = request.ConservationProcesses.Select(p => new ConservationProcess
                {
                    Id = p.IdProcess,
                    Symbologies = p.Symbologies.Select(s => new Symbology
                    {
                        Id = s.Id,
                        Name = s.Translate,
                        IdProcess = p.IdProcess
                    }).ToList()
                }).ToList()
            };

            await _repository.AddLegislationAsync(legislation);

            return Ok(legislation);
        }

    }
}
