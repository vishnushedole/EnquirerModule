using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EnquiryModule.Models;

[ExcludeFromCodeCoverage]
public partial class Manager
{
    public int EmpId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string AddressLine { get; set; } = null!;

    public int Pincode { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int? NoOfEnq { get; set; }

    public virtual ICollection<Enquirer> Enquirers { get; set; } = new List<Enquirer>();

    public virtual ICollection<MgrAssignedEnquire> MgrAssignedEnquires { get; set; } = new List<MgrAssignedEnquire>();
}
