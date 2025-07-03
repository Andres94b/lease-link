using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRMSProject.Models;

public partial class Role
{
    [Key]
    [Display(Name = "Role Id")]
    public int RoleId { get; set; }

    [Display(Name = "Role Name")]
    [Required(ErrorMessage = "Role Name is required.")]
    [StringLength(255, ErrorMessage = "Role Name cannot exceed 255 characters.")] // Added StringLength as RoleName is likely a string with a reasonable max length
    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}