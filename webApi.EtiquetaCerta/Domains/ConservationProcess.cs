using System;
using System.Collections.Generic;

namespace webApi.EtiquetaCerta.Domains;

public partial class ConservationProcess
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ProcessInLegislation> ProcessInLegislations { get; set; } = new List<ProcessInLegislation>();

    public virtual ICollection<Symbology> Symbologies { get; set; } = new List<Symbology>();
}
