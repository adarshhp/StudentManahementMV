using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AssignementProject.ViewModels;

/// <summary>
/// Represents the student registration form, including personal details,
/// login details, profile image upload, and qualifications.
/// </summary>
public class StudentRegistrationViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StudentRegistrationViewModel"/> class.
    /// </summary>
    public StudentRegistrationViewModel()
    {
        Qualifications = new List<QualificationViewModel>();
    }

    /// <summary>
    /// Gets or sets the internal student primary key.
    /// </summary>
    public int StudentId { get; set; }

    /// <summary>
    /// Gets or sets the auto-generated student code displayed on the form.
    /// </summary>
    [Display(Name = "Student ID")]
    public string StudentCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the student's first name.
    /// </summary>
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the student's last name.
    /// </summary>
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the student's age.
    /// </summary>
    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
    [Display(Name = "Age")]
    public int? Age { get; set; }

    /// <summary>
    /// Gets or sets the student's date of birth.
    /// </summary>
    [DataType(DataType.Date)]
    [Display(Name = "Date Of Birth")]
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the student's gender.
    /// </summary>
    [StringLength(20, ErrorMessage = "Gender cannot exceed 20 characters.")]
    [Display(Name = "Gender")]
    public string? Gender { get; set; }

    /// <summary>
    /// Gets or sets the student's email address.
    /// </summary>
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the student's phone number.
    /// </summary>
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    [RegularExpression(@"^[0-9+\-\s()]{7,15}$", ErrorMessage = "Please enter a valid phone number.")]
    [Display(Name = "Phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the student's address.
    /// </summary>
    [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
    [Display(Name = "Address")]
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the login username.
    /// </summary>
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 50 characters.")]
    [Display(Name = "Username")]
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the login password.
    /// </summary>
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).{6,}$", ErrorMessage = "Password must contain at least one letter and one number.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the uploaded profile photo.
    /// </summary>
    [Display(Name = "Profile Photo")]
    public IFormFile? ProfilePhoto { get; set; }

    /// <summary>
    /// Gets or sets the existing saved profile image path.
    /// </summary>
    public string? ProfileImagePath { get; set; }

    /// <summary>
    /// Gets or sets the qualification rows entered by the user.
    /// </summary>
    public List<QualificationViewModel> Qualifications { get; set; }
}
