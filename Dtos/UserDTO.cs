using System.ComponentModel.DataAnnotations;

public class UserDTO
{
  [Required(ErrorMessage = "Name is required")]
  [MaxLength(255)]
  public required string Name { get; set; }
  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Invalid Email Address")]
  [MaxLength(255)]
  public required string Email { get; set; }
  [Required(ErrorMessage = "Password is required")]
  [MaxLength(255)]
  public required string Password { get; set; }
  public DateTime CreatedAt { get; set; }
}