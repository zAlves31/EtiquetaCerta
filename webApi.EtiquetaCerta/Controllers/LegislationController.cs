using Microsoft.AspNetCore.Mvc;
using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Dtos;
using webApi.EtiquetaCerta.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class LegislationController : ControllerBase
{
    private readonly ILegislationRepository _legislationRepository;
    private readonly IConservationProcessRepository _conservationProcessRepository;
    private readonly ISymbologyRepository _symbologyRepository;
    private readonly IProcessInLegislationRepository _processInLegislationRepository;
    private readonly ISymbologyTranslateRepository _symbologyTranslateRepository;

    public LegislationController(
        ILegislationRepository legislationRepository,
        IConservationProcessRepository conservationProcessRepository,
        ISymbologyRepository symbologyRepository,
        IProcessInLegislationRepository processInLegislationRepository,
        ISymbologyTranslateRepository symbologyTranslateRepository)
    {
        _legislationRepository = legislationRepository;
        _conservationProcessRepository = conservationProcessRepository;
        _symbologyRepository = symbologyRepository;
        _processInLegislationRepository = processInLegislationRepository;
        _symbologyTranslateRepository = symbologyTranslateRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLegislation([FromBody] LegislationRequest request)
    {
        try
        {
            // Criar nova legislação
            var legislation = new Legislation
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                OfficialLanguage = request.OfficialLanguage,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            // Adicionar legislação ao banco de dados usando o repositório
            await _legislationRepository.AddAsync(legislation);

            // Adicionar associações de processos de conservação
            foreach (var processRequest in request.ConservationProcesses)
            {
                // Verifica se o processo de conservação existe
                var process = await _conservationProcessRepository.GetByIdAsync(processRequest.IdProcess);

                if (process == null)
                {
                    return BadRequest($"Processo de conservação com ID {processRequest.IdProcess} não foi encontrado.");
                }

                // Adicionar associação à tabela ProcessInLegislation
                var processInLegislation = new ProcessInLegislation
                {
                    Id = Guid.NewGuid(),
                    IdProcess = process.Id,
                    IdLegislation = legislation.Id
                };

                await _processInLegislationRepository.AddAsync(processInLegislation);

                // Valida e associa as simbologias
                foreach (var symbologyRequest in processRequest.Symbology)
                {
                    var symbology = await _symbologyRepository.GetByIdAsync(symbologyRequest.Id);

                    if (symbology == null)
                    {
                        return BadRequest($"Simbologia com ID {symbologyRequest.Id} não foi encontrada.");
                    }

                    if (symbology.IdProcess != process.Id)
                    {
                        return BadRequest($"A simbologia '{symbologyRequest.Name}' pertence ao processo '{symbology.IdProcessNavigation?.Name}' e não pode ser associada ao processo '{process.Name}'.");
                    }

                    var symbologyTranslate = new SymbologyTranslate
                    {
                        Id = Guid.NewGuid(),
                        IdSymbology = symbology.Id,
                        IdLegislation = legislation.Id,
                        SymbologyTranslate1 = symbologyRequest.Name
                    };

                    await _symbologyTranslateRepository.AddAsync(symbologyTranslate);
                }
            }

            // Retornar a legislação criada como resposta
            return Ok(legislation);
        }
        catch (Exception ex)
        {
            // Log para erro
            Console.WriteLine($"Erro ao criar legislação: {ex.Message}");
            return StatusCode(500, "Erro interno do servidor");
        }
    }


    [HttpGet]
    public async Task<ActionResult<LegislationResponseDto>> GetLegislations()
    {
        try
        {
            // Obtenha as legislações do repositório
            var legislations = await _legislationRepository.ListAsync();

            // Mapeie para DTOs
            var legislationDtos = legislations.Select(l => new LegislationDto
            {
                Id = l.Id,
                Name = l.Name,
                OfficialLanguage = l.OfficialLanguage,
                ConservationProcess = l.ProcessInLegislations.Select(cp => new ConservationProcessDto
                {
                    IdProcess = cp.IdProcess ?? Guid.Empty,
                    Symbology = l.SymbologyTranslates
                        .Where(st => st.IdSymbologyNavigation != null && st.IdSymbologyNavigation.IdProcess == cp.IdProcess)
                        .Select(st => new SymbologyDto
                        {
                            Id = st.Id,
                            Translate = st.SymbologyTranslate1
                        }).ToList()
                }).ToList(),
                CreatedAt = l.CreatedAt,
                UpdatedAt = l.UpdatedAt
            }).ToList();

            return Ok(new LegislationResponseDto { Legislations = legislationDtos });
        }
        catch (Exception ex)
        {
            // Log para erro
            Console.WriteLine($"Erro ao obter legislações: {ex.Message}");
            return StatusCode(500, "Erro interno do servidor");
        }
    }

}
