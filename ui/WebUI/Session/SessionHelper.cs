using System.ComponentModel.DataAnnotations;

namespace WebUI.Session;

public class SessionHelper
{
    public Guid Id { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(35, ErrorMessage = "Must be between 8 and 35 characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
