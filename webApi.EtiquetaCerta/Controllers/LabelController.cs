using Microsoft.AspNetCore.Mvc;

using webApi.EtiquetaCerta.Domains;
using webApi.EtiquetaCerta.Dtos;
using webApi.EtiquetaCerta.Interfaces;

namespace webApi.EtiquetaCerta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LabelController : ControllerBase
    {
        private readonly ILabelRepository _labelRepository;
        private readonly ILegislationRepository _legislationRepository;
        private readonly ILabelSymbologyRepository _labelSymbologyRepository;
        private readonly ISymbologyRepository _symbologyRepository;
        private readonly IProcessInLegislationRepository _processInLegislationRepository;
        private readonly ILogger<LabelController> _logger;

        public LabelController(
            ILabelRepository labelRepository,
            ILegislationRepository legislationRepository,
            ILabelSymbologyRepository labelSymbologyRepository,
            ISymbologyRepository symbologyRepository,
            IProcessInLegislationRepository processInLegislationRepository,
            ILogger<LabelController> logger)

        {
            _labelRepository = labelRepository;
            _legislationRepository = legislationRepository;
            _labelSymbologyRepository = labelSymbologyRepository;
            _symbologyRepository = symbologyRepository;
            _processInLegislationRepository = processInLegislationRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLabel([FromBody] LabelPostDto request)
        {
            try
            {
                // Verifica se a legislação existe
                var legislation = await _labelRepository.GetByIdAsync(request.IdLegislation);
                if (legislation == null)
                {
                    return NotFound($"Legislação com ID {request.IdLegislation} não foi encontrada.");
                }

                // Cria uma nova etiqueta (Label)
                var label = new Label
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    IdLegislation = request.IdLegislation, // Referência à legislação
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                // Adiciona a etiqueta ao banco de dados
                await _labelRepository.AddAsync(label);

                // Retorna a resposta com CreatedAt (201 Created)
                return CreatedAtAction(nameof(CreateLabel), new { id = label.Id }, label);
            }
            catch (Exception ex)
            {
                // Logging para erro de criação
                Console.WriteLine($"Erro ao criar etiqueta: {ex.Message}");
                return StatusCode(500, "Erro ao criar etiqueta.");
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateLabelSymbologies(Guid id, [FromBody] LabelSymbologyUpdateDto request)
        {
            try
            {
                _logger.LogInformation($"Iniciando atualização de simbologias para a etiqueta com ID {id}.");

                // Verifica se a etiqueta (Label) com a legislação associada existe
                var labelWithLegislation = await _labelRepository.GetLabelWithLegislationByIdAsync(id);
                if (labelWithLegislation == null)
                {
                    _logger.LogWarning($"Etiqueta com ID {id} não foi encontrada.");
                    return NotFound($"Etiqueta com ID {id} não foi encontrada.");
                }

                _logger.LogInformation($"Etiqueta com ID {id} encontrada, verificando legislação vinculada.");

                var legislation = labelWithLegislation.IdLegislationNavigation;
                if (legislation == null)
                {
                    _logger.LogWarning($"Legislação associada à etiqueta com ID {id} não foi encontrada.");
                    return BadRequest($"Legislação associada à etiqueta com ID {id} não foi encontrada.");
                }

                _logger.LogInformation($"Legislação com ID {legislation.Id} vinculada à etiqueta.");

                // Obtém os processos de conservação vinculados à legislação
                var processesInLegislation = await _processInLegislationRepository.GetByLegislationIdAsync(legislation.Id);
                var processIds = processesInLegislation.Select(p => p.IdProcess).ToList();

                _logger.LogInformation($"Processos associados à legislação: {string.Join(", ", processIds)}");

                // Verifica se as simbologias selecionadas pertencem aos processos da legislação
                var invalidSymbologies = new List<Guid>();
                foreach (var symbologyId in request.SelectedSymbology)
                {
                    _logger.LogInformation($"Verificando simbologia com ID {symbologyId}.");

                    var symbology = await _symbologyRepository.GetByIdAsync(symbologyId);
                    if (symbology == null)
                    {
                        _logger.LogWarning($"Simbologia com ID {symbologyId} não foi encontrada.");
                        return BadRequest($"Simbologia com ID {symbologyId} não foi encontrada.");
                    }

                    if (!processIds.Contains(symbology.IdProcess))
                    {
                        _logger.LogWarning($"Simbologia com ID {symbologyId} não está associada aos processos da legislação.");
                        invalidSymbologies.Add(symbologyId);
                    }
                }

                if (invalidSymbologies.Any())
                {
                    _logger.LogWarning($"Simbologias inválidas associadas à etiqueta com ID {id}: {string.Join(", ", invalidSymbologies)}");
                    return BadRequest($"Algumas simbologias selecionadas não estão associadas aos processos da legislação. IDs inválidos: {string.Join(", ", invalidSymbologies)}");
                }

                // Remove as simbologias associadas anteriormente à etiqueta
                _logger.LogInformation($"Removendo simbologias anteriores da etiqueta com ID {id}.");
                await _labelSymbologyRepository.RemoveByLabelIdAsync(id);

                // Adiciona as novas simbologias
                foreach (var symbologyId in request.SelectedSymbology)
                {
                    var labelSymbology = new LabelSymbology
                    {
                        Id = Guid.NewGuid(),
                        IdLabel = id,
                        IdSymbology = symbologyId
                    };

                    _logger.LogInformation($"Adicionando simbologia com ID {symbologyId} à etiqueta com ID {id}.");
                    await _labelSymbologyRepository.AddAsync(labelSymbology);
                }

                _logger.LogInformation($"Simbologias atualizadas com sucesso para a etiqueta com ID {id}.");
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar simbologias da etiqueta com ID {id}");
                return StatusCode(500, "Erro ao atualizar simbologias da etiqueta.");
            }
        }



        [HttpGet]
        public async Task<ActionResult<List<LabelDto>>> GetAllLabelsWithLegislationAndSymbology()
        {
            try
            {
                var labels = await _labelRepository.GetAllLabelsWithLegislationAndSymbologyAsync();

                // Prepara o payload esperado
                var response = new
                {
                    label = labels
                };

                return Ok(response); // Retorna um status 200 OK com o payload esperado
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }



    }
}
