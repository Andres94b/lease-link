using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRMSProject.Models;

public partial class User
{
    [Key]
    [Display(Name = "User Id")]
    public int UserId { get; set; }

    [Display(Name = "Full Name")]
    [StringLength(255, ErrorMessage = "Full Name cannot exceed 255 characters.")] // Added StringLength for better data integrity
    public string? UserFullName { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters.")] // Added StringLength for security and data integrity
    public string? UserPassword { get; set; }

    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")] // Added StringLength for data integrity
    public string? Email { get; set; }

    [Display(Name = "Phone Number")]
    [StringLength(20, ErrorMessage = "Phone Number cannot exceed 20 characters.")] // Added StringLength for data integrity
    public string? PhoneNumber { get; set; }

    [Display(Name = "Owned Apartments")]
    public virtual ICollection<Apartment> ApartmentOwners { get; set; } = new List<Apartment>();

    [Display(Name = "Rented Apartments")]
    public virtual ICollection<Apartment> ApartmentTenants { get; set; } = new List<Apartment>();

    [Display(Name = "Appointments Managed")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [Display(Name = "Events Related To")]
    public virtual ICollection<Event> EventRelatedToNavigations { get; set; } = new List<Event>();

    [Display(Name = "Events Reported")]
    public virtual ICollection<Event> EventReportedByNavigations { get; set; } = new List<Event>();

    //[Display(Name = "Listings Managed")]
    //public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();

    [Display(Name = "Messages Received")]
    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    [Display(Name = "Messages Sent")]
    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    [Display(Name = "Payments Made")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [Display(Name = "Requests Made")]
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    [Display(Name = "Roles")]
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<Apartment> AparmentManagers { get; set; } = new List<Apartment>();

}