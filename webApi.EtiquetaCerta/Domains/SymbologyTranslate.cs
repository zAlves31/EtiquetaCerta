using System;
using System.Collections.Generic;

namespace webApi.EtiquetaCerta.Domains;

public partial class SymbologyTranslate
{
    public Guid Id { get; set; }

    public Guid? IdSymbology { get; set; }

    public Guid? IdLegislation { get; set; }

    public string? SymbologyTranslate1 { get; set; }

    public virtual Legislation? IdLegislationNavigation { get; set; }

    public virtual Symbology? IdSymbologyNavigation { get; set; }
}
