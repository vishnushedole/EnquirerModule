using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EnquiryModule.Models;

[ExcludeFromCodeCoverage]
public partial class Document
{
    public int DocId { get; set; }
    
    [Required]
    public int EnqId { get; set; }

    public int? CustId { get; set; }

    [Required]
    public int DocTypeId { get; set; }

    public int Status { get; set; }

    [Required]  
    public byte[] Doc { get; set; } = null!;

    public virtual DocType DocType { get; set; } = null!;

    public virtual Enquirer Enq { get; set; } = null!;
}
