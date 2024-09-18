using System;
using System.Collections.Generic;

namespace webApi.EtiquetaCerta.Domains;

using Newtonsoft.Json; // Certifique-se de incluir este namespace

public partial class Legislation
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? OfficialLanguage { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
     
    [JsonIgnore]
    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();

    [JsonIgnore]
    public virtual ICollection<ProcessInLegislation> ProcessInLegislations { get; set; } = new List<ProcessInLegislation>();

    [JsonIgnore]
    public virtual ICollection<SymbologyTranslate> SymbologyTranslates { get; set; } = new List<SymbologyTranslate>();
}


