using AssignementProject.Data;
using AssignementProject.Interfaces.Repositories;
using AssignementProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignementProject.Repositories;

/// <summary>
/// Provides Entity Framework Core data access operations for login validation.
/// </summary>
public class LoginRepository : ILoginRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginRepository"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public LoginRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<Student?> ValidateLoginAsync(string username, string password)
    {
        try
        {
            string normalizedUsername = username.Trim().ToLower();
            string normalizedPassword = password.Trim();

            return await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(student =>
                    student.Username != null
                    && student.Password != null
                    && student.Username.Trim().ToLower() == normalizedUsername
                    && student.Password.Trim() == normalizedPassword
                    && !student.IsDeleted);
        }
        catch
        {
            throw;
        }
    }
}
