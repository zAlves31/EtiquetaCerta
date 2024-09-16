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

        public LabelController(
            ILabelRepository labelRepository,
            ILegislationRepository legislationRepository,
            ILabelSymbologyRepository labelSymbologyRepository,
            ISymbologyRepository symbologyRepository,
            IProcessInLegislationRepository processInLegislationRepository)
        {
            _labelRepository = labelRepository;
            _legislationRepository = legislationRepository;
            _labelSymbologyRepository = labelSymbologyRepository;
            _symbologyRepository = symbologyRepository;
            _processInLegislationRepository = processInLegislationRepository;
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
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
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
                // Verifica se a etiqueta (Label) existe
                var label = await _labelRepository.GetByIdAsync(id);
                if (label == null)
                {
                    return NotFound($"Etiqueta com ID {id} não foi encontrada.");
                }

                // Obtém a legislação associada à etiqueta
                var legislation = await _labelRepository.GetByIdAsync(id);
                ;
                if (legislation == null)
                {
                    return BadRequest($"Legislação associada à etiqueta com ID não foi encontrada.");
                }

                // Obtém os processos de conservação vinculados à legislação
                var processesInLegislation = await _processInLegislationRepository.GetByLegislationIdAsync(legislation.Id);
                var processIds = processesInLegislation.Select(p => p.IdProcess).ToList();

                // Verifica se as simbologias selecionadas pertencem aos processos da legislação
                var invalidSymbologies = new List<Guid>();
                foreach (var symbologyId in request.SelectedSymbology)
                {
                    var symbology = await _symbologyRepository.GetByIdAsync(symbologyId);
                    if (symbology == null)
                    {
                        return BadRequest($"Simbologia com ID {symbologyId} não foi encontrada.");
                    }

                    if (!processIds.Contains(symbology.IdProcess))
                    {
                        invalidSymbologies.Add(symbologyId);
                    }
                }

                if (invalidSymbologies.Any())
                {
                    return BadRequest($"Algumas simbologias selecionadas não estão associadas aos processos da legislação. IDs inválidos: {string.Join(", ", invalidSymbologies)}");
                }

                // Remove as simbologias associadas anteriormente à etiqueta
                await _labelSymbologyRepository.RemoveByLabelIdAsync(label.Id);

                // Adiciona as novas simbologias
                foreach (var symbologyId in request.SelectedSymbology)
                {
                    var labelSymbology = new LabelSymbology
                    {
                        Id = Guid.NewGuid(),
                        IdLabel = label.Id,
                        IdSymbology = symbologyId
                    };
                    await _labelSymbologyRepository.AddAsync(labelSymbology);
                }

                // Retorna uma resposta de sucesso
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                // Log para erros
                Console.WriteLine($"Erro ao atualizar simbologias da etiqueta com ID {id}: {ex.Message}");
                return StatusCode(500, "Erro ao atualizar simbologias da etiqueta.");
            }
        }
    }
}
