namespace webApi.EtiquetaCerta.Dtos
{

    public class LegislationRequest
    {
        public string Name { get; set; }
        public string OfficialLanguage { get; set; }
        public List<ProcessRequest> ConservationProcesses { get; set; }
    }

    public class ProcessRequest
    {
        public Guid ProcessId { get; set; }  // ID do processo já existente no banco
        public List<Guid> SymbologyIds { get; set; }  // Lista de IDs das simbologias
    }


}
