using System.ComponentModel.DataAnnotations;

namespace AssignementProject.Models;

/// <summary>
/// Represents a student registered in the school management system.
/// </summary>
public class Student
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Student"/> class.
    /// </summary>
    public Student()
    {
        Qualifications = new List<Qualification>();
        CreatedDate = DateTime.UtcNow;
        IsDeleted = false;
    }

    /// <summary>
    /// Gets or sets the primary key of the student.
    /// </summary>
    [Key]
    public int StudentId { get; set; }

    /// <summary>
    /// Gets or sets the auto-generated student code, for example STD0001.
    /// </summary>
    [StringLength(20, ErrorMessage = "Student code cannot exceed 20 characters.")]
    public string StudentCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the student's first name.
    /// </summary>
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the student's last name.
    /// </summary>
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    public string? LastName { get; set; }

    /// <summary>
    /// Gets the student's full name.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}".Trim();

    /// <summary>
    /// Gets or sets the student's age.
    /// </summary>
    [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
    public int? Age { get; set; }

    /// <summary>
    /// Gets or sets the student's date of birth.
    /// </summary>
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the student's gender.
    /// </summary>
    [StringLength(20, ErrorMessage = "Gender cannot exceed 20 characters.")]
    public string? Gender { get; set; }

    /// <summary>
    /// Gets or sets the student's email address.
    /// </summary>
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the student's phone number.
    /// </summary>
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the student's address.
    /// </summary>
    [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the login username assigned to the student.
    /// </summary>
    [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the student's password.
    /// </summary>
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the relative path of the uploaded profile image.
    /// </summary>
    [StringLength(250, ErrorMessage = "Profile image path cannot exceed 250 characters.")]
    public string? ProfileImagePath { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the student record was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the student record was last updated.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the student record is soft deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the qualifications associated with the student.
    /// </summary>
    public ICollection<Qualification> Qualifications { get; set; }
}
