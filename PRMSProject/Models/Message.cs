using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRMSProject.Models;

public partial class Message
{
    [Key]
    [Display(Name = "Message Id")]
    [Required(ErrorMessage = "Message Id is required.")]
    public string MessageId { get; set; } = null!;

    [Display(Name = "Sender Id")]
    [Required(ErrorMessage = "Sender Id is required.")]
    public int SenderId { get; set; }

    [Display(Name = "Receiver Id")]
    [Required(ErrorMessage = "Receiver Id is required.")]
    public int ReceiverId { get; set; }

    [Display(Name = "Message Subject")]
    public string? MessageSubject { get; set; }

    [Display(Name = "Message Text")]
    public string? MessageText { get; set; }

    [Display(Name = "Sent At")]
    public DateTime? SentAt { get; set; }

    [Display(Name = "Receiver")]
    public virtual User? Receiver { get; set; }

    [Display(Name = "Sender")]
    public virtual User? Sender { get; set; }
}
