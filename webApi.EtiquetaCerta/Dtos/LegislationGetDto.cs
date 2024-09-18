using Newtonsoft.Json;

public class LegislationDto
{
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("official_language")]
    public string? OfficialLanguage { get; set; }

    [JsonProperty("conservation_process")]
    public List<ConservationProcessDto> ConservationProcess { get; set; } = new List<ConservationProcessDto>();

    [JsonProperty("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonProperty("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}

public class ConservationProcessDto
{
    [JsonProperty("id_process")]
    public Guid IdProcess { get; set; }

    [JsonProperty("symbology")]
    public List<SymbologyDto> Symbology { get; set; } = new List<SymbologyDto>();
}

public class SymbologyDto
{
    [JsonProperty("id")]
    public Guid Id { get; set; }


    [JsonProperty("translate")]
    public string? Translate { get; set; }
}


public class LegislationResponseDto
{
    [JsonProperty("legislations")]
    public List<LegislationDto> Legislations { get; set; } = new List<LegislationDto>();
}
