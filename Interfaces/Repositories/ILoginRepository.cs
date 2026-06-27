using AssignementProject.Models;

namespace AssignementProject.Interfaces.Repositories;

/// <summary>
/// Defines data access operations required for login validation.
/// </summary>
public interface ILoginRepository
{
    /// <summary>
    /// Validates a student's login credentials.
    /// </summary>
    /// <param name="username">The username entered by the user.</param>
    /// <param name="password">The password entered by the user.</param>
    /// <returns>The matching student when credentials are valid; otherwise, null.</returns>
    Task<Student?> ValidateLoginAsync(string username, string password);
}
