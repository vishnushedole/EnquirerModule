using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace EnquiryModule.Models;

[ExcludeFromCodeCoverage]
public partial class Enquirer
{

    public int EnquiryId { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Addr { get; set; } = null!;

    [Required]
    [Phone]
    public string PhoneNo { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string City { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Stat { get; set; } = null!;

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Country { get; set; } = null!;

    [Required]
    [StringLength(6, MinimumLength = 1)]
    public string PinCode { get; set; } = null!;

    [Required]
    [StringLength(20)]
    public string MaritalStatus { get; set; } = null!;

    [Required]
    [StringLength(10)]
    public string Gender { get; set; } = null!;

    [Range(18, 120)]
    public int Age { get; set; }

    [Range(0, 2)]
    public int Status { get; set; }

    [Required]
    [DataType(DataType.Date)]
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
