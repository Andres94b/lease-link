using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRMSProject.Models;

public partial class Request
{
    [Key]
    [Display(Name = "Request Id")]
    [Required(ErrorMessage = "Request Id is required.")]
    public string RequestId { get; set; } = null!;

    [Display(Name = "User Id")]
    //[Required(ErrorMessage = "User Id is required.")]
    public int UserId { get; set; }

    [Display(Name = "Apartment Id")]
    public string? ApartmentId { get; set; }

    [Display(Name = "Request Text")]
    public string? RequestText { get; set; }

    [Display(Name = "Request Status")]
    public string? RequestStatus { get; set; }

    [Display(Name = "Created At")]
    public DateTime? CreatedAt { get; set; }

    [Display(Name = "Apartment")]
    public virtual Apartment? Apartment { get; set; }

    [Display(Name = "User")]
    public virtual User? User { get; set; }
}