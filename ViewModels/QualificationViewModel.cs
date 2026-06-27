using System.ComponentModel.DataAnnotations;

namespace AssignementProject.ViewModels;

/// <summary>
/// Represents a qualification row entered on the student registration form.
/// </summary>
public class QualificationViewModel
{
    /// <summary>
    /// Gets or sets the course name.
    /// </summary>
    [Required(ErrorMessage = "Course name is required.")]
    [StringLength(100, ErrorMessage = "Course name cannot exceed 100 characters.")]
    [Display(Name = "Course")]
    public string CourseName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the university or board name.
    /// </summary>
    [Required(ErrorMessage = "University is required.")]
    [StringLength(100, ErrorMessage = "University cannot exceed 100 characters.")]
    [Display(Name = "University")]
    public string University { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the passing year.
    /// </summary>
    [Required(ErrorMessage = "Passing year is required.")]
    [Range(1950, 2100, ErrorMessage = "Passing year must be between 1950 and 2100.")]
    [Display(Name = "Passing Year")]
    public int? PassingYear { get; set; }

    /// <summary>
    /// Gets or sets the percentage obtained.
    /// </summary>
    [Required(ErrorMessage = "Percentage is required.")]
    [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100.")]
    [Display(Name = "Percentage")]
    public decimal? Percentage { get; set; }
}
