using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EnquiryModule.Models;

[ExcludeFromCodeCoverage]
public partial class MgrAssignedEnquire
{
    public int MgrAssignedEnquiresId { get; set; }

    public int? EmpId { get; set; }

    public int? EnquiryId { get; set; }

    public bool? Isprocessed { get; set; }

    public virtual Manager? Emp { get; set; }

    public virtual Enquirer? Enquiry { get; set; }
}
