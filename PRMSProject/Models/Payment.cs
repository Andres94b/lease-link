using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRMSProject.Models;

public partial class Payment
{
    [Key]
    [Display(Name = "Payment Id")]
    [Required(ErrorMessage = "Payment Id is required.")]
    public string PaymentId { get; set; } = null!;

    [Display(Name = "Tenant Id")]
    [Required(ErrorMessage = "Tenant Id is required.")]
    public int TenantId { get; set; }

    [Display(Name = "Apartment Id")]
    [Required(ErrorMessage = "Apartment Id is required.")]
    public string ApartmentId { get; set; } = null!;

    [Display(Name = "Payment Date")]
    [Required(ErrorMessage = "Payment Date is required.")]
    public DateOnly PaymentDate { get; set; }

    [Display(Name = "Amount")]
    [Required(ErrorMessage = "Amount is required.")]
    public decimal Amount { get; set; }

    [Display(Name = "Payment Method")]
    public string? PaymentMethod { get; set; }

    [Display(Name = "Payment Status")]
    public string? PaymentStatus { get; set; }

    [Display(Name = "Apartment")]
    public virtual Apartment Apartment { get; set; } = null!;

    [Display(Name = "Tenant")]
    public virtual User Tenant { get; set; } = null!;
}