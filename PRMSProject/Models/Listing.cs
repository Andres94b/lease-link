using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRMSProject.Models;

public partial class Listing
{
    [Key]
    [Display(Name = "Listing Id")]
    [Required(ErrorMessage = "Listing Id is required.")]
    public string ListingId { get; set; } = null!;

    [Display(Name = "Apartment Id")]
    [Required(ErrorMessage = "Apartment Id is required.")]
    public string ApartmentId { get; set; } = null!;


    [Display(Name = "Price")]
    [Required(ErrorMessage = "Price is required.")]
    public decimal Price { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Available From")]
    public DateOnly? AvailableFrom { get; set; }

    [Display(Name = "Published Date")]
    public DateOnly? PublishedDate { get; set; }

    [Display(Name = "Expiry Date")]
    public DateOnly? ExpiryDate { get; set; }

    [Display(Name = "Listing Status")]
    public string? ListingStatus { get; set; }

    [Display(Name = "Title")]
    public string? Title { get; set; }

    [Display(Name = "Apartment")]
    public virtual Apartment Apartment { get; set; } = null!;

}
