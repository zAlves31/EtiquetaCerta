using System;
using System.Collections.Generic;

namespace webApi.EtiquetaCerta.Domains;

public partial class Legislation
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? OfficialLanguage { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();

    public virtual ICollection<ProcessInLegislation> ProcessInLegislations { get; set; } = new List<ProcessInLegislation>();

    public virtual ICollection<ConservationProcess> ConservationProcesses { get; set; } = new List<ConservationProcess>();

    public virtual ICollection<SymbologyTranslate> SymbologyTranslates { get; set; } = new List<SymbologyTranslate>();
}
