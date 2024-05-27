using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EnquiryModule.Models;

[ExcludeFromCodeCoverage]
public partial class DocType
{
    public int DocType1 { get; set; }

    public string DocName { get; set; } = null!;

    public bool Isactive { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
