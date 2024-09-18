using Newtonsoft.Json;

namespace webApi.EtiquetaCerta.Dtos
{
    public class LabelSymbologyUpdateDto
    {
        public List<Guid> SelectedSymbology { get; set; } = new List<Guid>();
    }

    public class SymbologyGetDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("id_process")]
        public Guid? IdProcess { get; set; } // Propriedade opcional

        [JsonProperty("translate")]
        public string Translate { get; set; }
    }




    public class LabelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid IdLegislation { get; set; } // Tratar como Guid não nulo
        public List<SymbologyGetDto> SelectedSymbology { get; set; }
    }
}
