using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRMSProject.Models;

public partial class Building
{
    [Key]
    [Display(Name = "Building Id")]
    [Required(ErrorMessage = "Building Id is required.")]
    public string BuildingId { get; set; } = null!;

    [Display(Name = "Building Name")]
    public string? BuildingName { get; set; }

    [Display(Name = "Building Address")]
    public string? BuildingAddress { get; set; }

    [Display(Name = "Building Phone Number")]
    public string? BuildingPhoneNumber { get; set; }

    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
}