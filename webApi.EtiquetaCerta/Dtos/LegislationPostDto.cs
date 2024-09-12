namespace webApi.EtiquetaCerta.Dtos
{

    public class LegislationPostDto
    {
        public string Name { get; set; }
        public string OfficialLanguage { get; set; }
        public List<ConservationProcessPostDto> ConservationProcesses { get; set; }
    }

    public class ConservationProcessPostDto
    {
        public Guid IdProcess { get; set; }
        public List<SymbologyPostDto> Symbologies { get; set; }
    }

    public class SymbologyPostDto
    {
        public Guid Id { get; set; }
        public string Translate { get; set; }  // Correspondente à propriedade Translate
    }

}
