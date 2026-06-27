using AssignementProject.Models;
using AssignementProject.ViewModels;

namespace AssignementProject.Interfaces.Services;

/// <summary>
/// Defines business operations for login validation.
/// </summary>
public interface ILoginService
{
    /// <summary>
    /// Validates the submitted login details.
    /// </summary>
    /// <param name="model">The login form data.</param>
    /// <returns>The authenticated student when valid; otherwise, null.</returns>
    Task<Student?> ValidateLoginAsync(LoginViewModel model);
}
