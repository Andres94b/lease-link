using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRMSProject.Models;

public partial class Event
{
    [Key]
    [Display(Name = "Event Id")]
    [Required(ErrorMessage = "Event Id is required.")]
    public string EventId { get; set; } = null!;

    [Display(Name = "Reported By")]
    [Required(ErrorMessage = "Reported By is required.")]
    public int ReportedBy { get; set; }

    [Display(Name = "Related To")]
    public int? RelatedTo { get; set; }

    [Display(Name = "Apartment Id")]
    public string? ApartmentId { get; set; }

    [Display(Name = "Event Type")]
    public string? EventType { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Created At")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Apartment")]
    public virtual Apartment? Apartment { get; set; }

    [Display(Name = "Related To")]
    public virtual User? RelatedToNavigation { get; set; }

    [Display(Name = "Reported By")]
    public virtual User ReportedByNavigation { get; set; } = null!;
}