using System;
using System.Collections.Generic;

namespace webApi.EtiquetaCerta.Domains;

public partial class Symbology
{
    public Guid Id { get; set; }

    public Guid? IdProcess { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Url { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ConservationProcess? IdProcessNavigation { get; set; }

    public virtual ICollection<LabelSymbology> LabelSymbologies { get; set; } = new List<LabelSymbology>();

    public virtual ICollection<SymbologyTranslate> SymbologyTranslates { get; set; } = new List<SymbologyTranslate>();
}
