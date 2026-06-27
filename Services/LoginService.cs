using AssignementProject.Interfaces.Repositories;
using AssignementProject.Interfaces.Services;
using AssignementProject.Models;
using AssignementProject.ViewModels;

namespace AssignementProject.Services;

/// <summary>
/// Provides business operations for login validation.
/// </summary>
public class LoginService : ILoginService
{
    private readonly ILoginRepository _loginRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginService"/> class.
    /// </summary>
    /// <param name="loginRepository">The login repository.</param>
    public LoginService(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    /// <inheritdoc />
    public async Task<Student?> ValidateLoginAsync(LoginViewModel model)
    {
        try
        {
            return await _loginRepository.ValidateLoginAsync(
                model.Username.Trim(),
                model.Password.Trim());
        }
        catch
        {
            throw;
        }
    }
}
