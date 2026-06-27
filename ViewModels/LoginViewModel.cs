using System.ComponentModel.DataAnnotations;

namespace AssignementProject.ViewModels;

/// <summary>
/// Represents the login form submitted by a user.
/// </summary>
public class LoginViewModel
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
    [Display(Name = "Username")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;
}
