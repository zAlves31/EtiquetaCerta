namespace webApi.EtiquetaCerta.Dtos
{
    public class LabelSymbologyUpdateDto
    {
        public List<Guid> SelectedSymbology { get; set; } = new List<Guid>();
    }

    public class LabelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid IdLegislation { get; set; }
        public List<SymbologyDto> SelectedSymbology { get; set; } = new List<SymbologyDto>();
    }

    public class SymbologyDto
    {
        public Guid Id { get; set; }
        public Guid IdProcess { get; set; }
        public string Translate { get; set; }
    }
}
