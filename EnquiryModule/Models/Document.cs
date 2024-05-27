using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EnquiryModule.Models;

[ExcludeFromCodeCoverage]
public partial class Document
{
    public int DocId { get; set; }

    public int EnqId { get; set; }

    public int? CustId { get; set; }

    public int DocTypeId { get; set; }

    public int Status { get; set; }

    public byte[] Doc { get; set; } = null!;

    public virtual DocType DocType { get; set; } = null!;

    public virtual Enquirer Enq { get; set; } = null!;
}
