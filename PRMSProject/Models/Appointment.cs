using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRMSProject.Models;

public partial class Appointment
{
    [Key]
    [Display(Name = "Appointment Id")]
    [Required(ErrorMessage = "Appointment Id is required.")]
    public string AppointmentId { get; set; } = null!;

    [Display(Name = "Apartment Id")]
    [Required(ErrorMessage = "Apartment Id is required.")]
    public string ApartmentId { get; set; } = null!;

    [Display(Name = "Manager Id")]
    [Required(ErrorMessage = "Manager Id is required.")]
    public int ManagerId { get; set; }

    [Display(Name = "Visitor Name")]
    public string? VisitorName { get; set; }

    [Display(Name = "Visitor Phone")]
    public string? VisitorPhone { get; set; }

    [Display(Name = "Appointment Date Time")]
    public DateTime? AppointmentDateTime { get; set; }

    [Display(Name = "Apartment")]
    public virtual Apartment Apartment { get; set; } = null!;

    [Display(Name = "Manager")]
    public virtual User Manager { get; set; } = null!;
}