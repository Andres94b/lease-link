using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRMSProject.Models;

public partial class Apartment
{
    [Key]
    [Display(Name = "Apartment Id")]
    [Required(ErrorMessage = "Apartment Id is required.")]
    public string ApartmentId { get; set; } = null!;

    [Display(Name = "Building Id")]
    [Required(ErrorMessage = "Building Id is required.")]
    public string BuildingId { get; set; } = null!;

    [Display(Name = "Owner Id")]
    public int? OwnerId { get; set; }

    [Display(Name = "Tenant Id")]
    public int? TenantId { get; set; }

    [Display(Name = "Apartment Number")]
    public string? ApartmentNumber { get; set; }

    [Display(Name = "Apartment Interior")]
    public string? ApartmentInterior { get; set; }

    [Display(Name = "Floor Number")]
    public int? FloorNumber { get; set; }

    [Display(Name = "Area")]
    public string? Area { get; set; }

    [Display(Name = "Bedroom Amount")]
    public int? BedroomAmount { get; set; }

    [Display(Name = "Bathrooms Amount")]
    public int? BathroomsAmount { get; set; }

    [Display(Name = "Garage Amount")]
    public int? GarageAmount { get; set; }

    [Display(Name = "Deposit Amount")]
    public int? DepositAmount { get; set; }

    [Display(Name = "Balcony Amount")]
    public int? BalconyAmount { get; set; }

    [Display(Name = "Room Amount")]
    public decimal? RoomAmount { get; set; }

    [Display(Name = "Is Rented")]
    public bool? IsRented { get; set; }

    [Display(Name = "Pictures Path")]
    public string? PicturesPath { get; set; }

    [Display(Name = "Manager ID")]
    public int? ManagerId { get; set; }

    [ForeignKey("ManagerId")]
    public virtual User? Manager { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [Display(Name = "Building")]
    public virtual Building Building { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();

    [Display(Name = "Owner")]
    public virtual User? Owner { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    [Display(Name = "Tenant")]
    public virtual User? Tenant { get; set; }
}