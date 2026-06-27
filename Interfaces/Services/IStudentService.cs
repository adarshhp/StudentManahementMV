using AssignementProject.Models;
using AssignementProject.ViewModels;

namespace AssignementProject.Interfaces.Services;

/// <summary>
/// Defines business operations for student management.
/// </summary>
public interface IStudentService
{
    /// <summary>
    /// Gets all active students.
    /// </summary>
    /// <returns>A list of active students.</returns>
    Task<List<Student>> GetAllStudentsAsync();

    /// <summary>
    /// Gets a student by primary key.
    /// </summary>
    /// <param name="id">The student primary key.</param>
    /// <returns>The matching student, or null if no record exists.</returns>
    Task<Student?> GetStudentByIdAsync(int id);

    /// <summary>
    /// Gets a student by username.
    /// </summary>
    /// <param name="username">The student's username.</param>
    /// <returns>The matching student, or null if no record exists.</returns>
    Task<Student?> GetStudentByUsernameAsync(string username);

    /// <summary>
    /// Registers a new student.
    /// </summary>
    /// <param name="model">The student registration form data.</param>
    /// <returns>True when the student is registered successfully.</returns>
    Task<bool> RegisterStudentAsync(StudentRegistrationViewModel model);

    /// <summary>
    /// Updates an existing student.
    /// </summary>
    /// <param name="model">The updated student form data.</param>
    /// <returns>True when the student is updated successfully; otherwise, false.</returns>
    Task<bool> UpdateStudentAsync(StudentRegistrationViewModel model);

    /// <summary>
    /// Deletes a student by primary key.
    /// </summary>
    /// <param name="id">The student primary key.</param>
    /// <returns>True when the operation completes successfully.</returns>
    Task<bool> DeleteStudentAsync(int id);

    /// <summary>
    /// Generates the next student code.
    /// </summary>
    /// <returns>The generated student code.</returns>
    Task<string> GenerateStudentCodeAsync();
}
