using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignementProject.Models;

/// <summary>
/// Represents an academic qualification completed by a student.
/// </summary>
public class Qualification
{
    /// <summary>
    /// Gets or sets the primary key of the qualification.
    /// </summary>
    [Key]
    public int QualificationId { get; set; }

    /// <summary>
    /// Gets or sets the student primary key associated with this qualification.
    /// </summary>
    public int StudentId { get; set; }

    /// <summary>
    /// Gets or sets the course name.
    /// </summary>
    [StringLength(100, ErrorMessage = "Course name cannot exceed 100 characters.")]
    public string? CourseName { get; set; }

    /// <summary>
    /// Gets or sets the university or board name.
    /// </summary>
    [StringLength(100, ErrorMessage = "University cannot exceed 100 characters.")]
    public string? University { get; set; }

    /// <summary>
    /// Gets or sets the year in which the course was passed.
    /// </summary>
    [Range(1950, 2100, ErrorMessage = "Passing year must be between 1950 and 2100.")]
    public int? PassingYear { get; set; }

    /// <summary>
    /// Gets or sets the percentage obtained in the qualification.
    /// </summary>
    [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100.")]
    [Column(TypeName = "decimal(5,2)")]
    public decimal? Percentage { get; set; }

    /// <summary>
    /// Gets or sets the student associated with this qualification.
    /// </summary>
    public Student? Student { get; set; }
}
