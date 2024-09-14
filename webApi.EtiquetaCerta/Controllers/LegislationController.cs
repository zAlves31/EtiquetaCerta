using Microsoft.AspNetCore.Mvc;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Dtos;
using webApi.EtiquetaCerta.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class LegislationController : ControllerBase
{
    private readonly ILegislationRepository _legislationRepository;
    private readonly ISymbologyRepository _symbologyRepository;

    public LegislationController(ILegislationRepository legislationRepository, ISymbologyRepository symbologyRepository)
    {
        _legislationRepository = legislationRepository;
        _symbologyRepository = symbologyRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLegislation([FromBody] LegislationRequest request)
    {
        // Validação se os processos e simbologias existem e estão vinculados corretamente
        foreach (var processRequest in request.ConservationProcesses)
        {
            var process = await _legislationRepository.GetByIdAsync(processRequest.ProcessId);

            if (process == null)
            {
                return BadRequest($"Processo de conservação com ID {processRequest.ProcessId} não foi encontrado.");
            }

            foreach (var symbologyId in processRequest.SymbologyIds)
            {
                var symbology = await _symbologyRepository.GetByIdAsync(symbologyId); // Usando o repositório de Symbology

                if (symbology == null)
                {
                    return BadRequest($"Simbologia com ID {symbologyId} não foi encontrada.");
                }

                if (symbology.IdProcess != process.Id)
                {
                    return BadRequest($"A simbologia '{symbology.Name}' pertence ao processo '{symbology.IdProcessNavigation.Name}' e não pode ser associada ao processo '{process.Name}'.");
                }
            }
        }

        // Criar nova legislação
        var legislation = new Legislation
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            OfficialLanguage = request.OfficialLanguage,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Adicionar legislação ao banco de dados usando o repositório
        await _legislationRepository.AddAsync(legislation);

        return Ok(legislation);
    }


}
