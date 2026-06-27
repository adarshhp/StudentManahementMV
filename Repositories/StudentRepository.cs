using AssignementProject.Data;
using AssignementProject.Interfaces.Repositories;
using AssignementProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignementProject.Repositories;

/// <summary>
/// Provides Entity Framework Core data access operations for students.
/// </summary>
public class StudentRepository : IStudentRepository
{
    private const string StudentCodePrefix = "STD";
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="StudentRepository"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<List<Student>> GetAllAsync()
    {
        try
        {
            return await _context.Students
                .AsNoTracking()
                .Where(student => !student.IsDeleted)
                .OrderBy(student => student.StudentCode)
                .ToListAsync();
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<Student?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Students
                .Include(student => student.Qualifications)
                .FirstOrDefaultAsync(student => student.StudentId == id && !student.IsDeleted);
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<Student?> GetByUsernameAsync(string username)
    {
        try
        {
            return await _context.Students
                .Include(student => student.Qualifications)
                .FirstOrDefaultAsync(student =>
                    student.Username == username && !student.IsDeleted);
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc />
    public async Task AddAsync(Student student)
    {
        try
        {
            await _context.Students.AddAsync(student);
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc />
    public Task UpdateAsync(Student student)
    {
        try
        {
            student.UpdatedDate = DateTime.UtcNow;
            _context.Students.Update(student);
            return Task.CompletedTask;
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        try
        {
            Student? student = await _context.Students
                .FirstOrDefaultAsync(existingStudent => existingStudent.StudentId == id && !existingStudent.IsDeleted);

            if (student is null)
            {
                return;
            }

            student.IsDeleted = true;
            student.UpdatedDate = DateTime.UtcNow;
            _context.Students.Update(student);
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<string> GenerateStudentCodeAsync()
    {
        try
        {
            string? latestStudentCode = await _context.Students
                .AsNoTracking()
                .Where(student => student.StudentCode.StartsWith(StudentCodePrefix))
                .OrderByDescending(student => student.StudentCode)
                .Select(student => student.StudentCode)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (!string.IsNullOrWhiteSpace(latestStudentCode)
                && latestStudentCode.Length > StudentCodePrefix.Length
                && int.TryParse(latestStudentCode[StudentCodePrefix.Length..], out int latestNumber))
            {
                nextNumber = latestNumber + 1;
            }

            return $"{StudentCodePrefix}{nextNumber:D4}";
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc />
    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            throw;
        }
    }
}
