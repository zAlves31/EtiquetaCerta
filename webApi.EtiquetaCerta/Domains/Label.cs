using System;
using System.Collections.Generic;

namespace webApi.EtiquetaCerta.Domains;

public partial class Label
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public Guid? IdLegislation { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Legislation? IdLegislationNavigation { get; set; }

    public virtual ICollection<LabelSymbology> LabelSymbologies { get; set; } = new List<LabelSymbology>();
}
