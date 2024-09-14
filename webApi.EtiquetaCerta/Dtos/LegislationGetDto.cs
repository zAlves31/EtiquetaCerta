namespace webApi.EtiquetaCerta.Dtos
{
    public class SymbologyDto
    {
        public Guid Id { get; set; }
        public string Translate { get; set; }
    }

    public class ConservationProcessDto
    {
        public Guid IdProcess { get; set; }
        public List<SymbologyDto> Symbology { get; set; }
    }

    public class LegislationResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OfficialLanguage { get; set; }
        public List<ConservationProcessDto> ConservationProcess { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class GetLegislationsResponseDto
    {
        public List<LegislationResponseDto> Legislations { get; set; }
    }

}
