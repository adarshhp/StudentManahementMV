using AssignementProject.Models;

namespace AssignementProject.Interfaces.Repositories;

/// <summary>
/// Defines data access operations for student records.
/// </summary>
public interface IStudentRepository
{
    /// <summary>
    /// Gets all active students.
    /// </summary>
    /// <returns>A list of active students.</returns>
    Task<List<Student>> GetAllAsync();

    /// <summary>
    /// Gets a student by primary key.
    /// </summary>
    /// <param name="id">The student primary key.</param>
    /// <returns>The matching student, or null if no student exists.</returns>
    Task<Student?> GetByIdAsync(int id);

    /// <summary>
    /// Gets a student by username.
    /// </summary>
    /// <param name="username">The student's username.</param>
    /// <returns>The matching student, or null if no student exists.</returns>
    Task<Student?> GetByUsernameAsync(string username);

    /// <summary>
    /// Adds a new student.
    /// </summary>
    /// <param name="student">The student to add.</param>
    Task AddAsync(Student student);

    /// <summary>
    /// Updates an existing student.
    /// </summary>
    /// <param name="student">The student to update.</param>
    Task UpdateAsync(Student student);

    /// <summary>
    /// Deletes a student by primary key.
    /// </summary>
    /// <param name="id">The student primary key.</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Generates the next student code in STD0001 format.
    /// </summary>
    /// <returns>The generated student code.</returns>
    Task<string> GenerateStudentCodeAsync();

    /// <summary>
    /// Persists pending changes to the database.
    /// </summary>
    Task SaveChangesAsync();
}
