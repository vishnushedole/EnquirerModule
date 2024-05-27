using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace EnquiryModule.Models;

[ExcludeFromCodeCoverage]
public partial class Enquirer
{
    public int EnquiryId { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Addr { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Stat { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string PinCode { get; set; } = null!;

    public string MaritalStatus { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public int Status { get; set; }

    public DateTime Dob { get; set; }

    public int? EmployeeId { get; set; }

    [BindNever]
    [JsonIgnore]
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    [BindNever]
    [JsonIgnore]
    public virtual Manager? Employee { get; set; }
    [BindNever]
    [JsonIgnore]
    public virtual ICollection<MgrAssignedEnquire> MgrAssignedEnquires { get; set; } = new List<MgrAssignedEnquire>();
}
