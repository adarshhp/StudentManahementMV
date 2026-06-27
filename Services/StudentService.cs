using AssignementProject.Interfaces.Repositories;
using AssignementProject.Interfaces.Services;
using AssignementProject.Models;
using AssignementProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace AssignementProject.Services;

/// <summary>
/// Provides business operations for student management.
/// </summary>
public class StudentService : IStudentService
{
    private static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".gif"];
    private readonly IStudentRepository _studentRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;

    /// <summary>
    /// Initializes a new instance of the <see cref="StudentService"/> class.
    /// </summary>
    /// <param name="studentRepository">The student repository.</param>
    /// <param name="webHostEnvironment">The web host environment.</param>
    public StudentService(
        IStudentRepository studentRepository,
        IWebHostEnvironment webHostEnvironment)
    {
        _studentRepository = studentRepository;
        _webHostEnvironment = webHostEnvironment;
    }

    /// <inheritdoc />
    public async Task<List<Student>> GetAllStudentsAsync()
    {
        try
        {
            return await _studentRepository.GetAllAsync();
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        try
        {
            return await _studentRepository.GetByIdAsync(id);
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<Student?> GetStudentByUsernameAsync(string username)
    {
        try
        {
            return await _studentRepository.GetByUsernameAsync(username);
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> RegisterStudentAsync(StudentRegistrationViewModel model)
    {
        try
        {
            ValidateStudentModel(model);

            string? profileImagePath = null;

            if (model.ProfilePhoto is not null)
            {
                profileImagePath = await SaveProfileImageAsync(model.ProfilePhoto);
            }

            Student student = MapToStudent(model);
            student.StudentCode = await _studentRepository.GenerateStudentCodeAsync();
            student.ProfileImagePath = profileImagePath;
            student.CreatedDate = DateTime.UtcNow;
            student.UpdatedDate = null;
            student.IsDeleted = false;

            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();

            return true;
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> UpdateStudentAsync(StudentRegistrationViewModel model)
    {
        try
        {
            ValidateStudentModel(model);

            Student? existingStudent = await _studentRepository.GetByIdAsync(model.StudentId);

            if (existingStudent is null)
            {
                return false;
            }

            string? profileImagePath = existingStudent.ProfileImagePath;

            if (model.ProfilePhoto is not null)
            {
                profileImagePath = await SaveProfileImageAsync(model.ProfilePhoto);
            }

            existingStudent.FirstName = NormalizeRequired(model.FirstName);
            existingStudent.LastName = NormalizeRequired(model.LastName);
            existingStudent.Age = model.Age;
            existingStudent.DateOfBirth = model.DateOfBirth;
            existingStudent.Gender = NormalizeRequired(model.Gender);
            existingStudent.Email = NormalizeRequired(model.Email);
            existingStudent.Phone = NormalizeRequired(model.Phone);
            existingStudent.Address = NormalizeRequired(model.Address);
            existingStudent.Username = NormalizeRequired(model.Username);
            existingStudent.Password = NormalizeRequired(model.Password);
            existingStudent.ProfileImagePath = profileImagePath;
            existingStudent.UpdatedDate = DateTime.UtcNow;
            existingStudent.IsDeleted = false;

            existingStudent.Qualifications.Clear();

            foreach (Qualification qualification in MapQualifications(model.Qualifications))
            {
                existingStudent.Qualifications.Add(qualification);
            }

            await _studentRepository.UpdateAsync(existingStudent);
            await _studentRepository.SaveChangesAsync();

            return true;
        }
        catch
        {
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteStudentAsync(int id)
    {
        try
        {
            await _studentRepository.DeleteAsync(id);
            await _studentRepository.SaveChangesAsync();

            return true;
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
            return await _studentRepository.GenerateStudentCodeAsync();
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Saves the uploaded profile image to the uploads folder.
    /// </summary>
    /// <param name="file">The uploaded profile image.</param>
    /// <returns>The relative image path, or null when no file is supplied.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the uploaded file extension is not allowed.</exception>
    public async Task<string?> SaveProfileImageAsync(IFormFile file)
    {
        try
        {
            if (file.Length == 0)
            {
                return null;
            }

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!AllowedImageExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Only JPG, JPEG, PNG, and GIF image files are allowed.");
            }

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = $"{Guid.NewGuid():N}{extension}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await using FileStream fileStream = new(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return $"/uploads/{uniqueFileName}";
        }
        catch
        {
            throw;
        }
    }

    private static Student MapToStudent(StudentRegistrationViewModel model)
    {
        return new Student
        {
            StudentId = model.StudentId,
            StudentCode = model.StudentCode,
            FirstName = NormalizeRequired(model.FirstName),
            LastName = NormalizeRequired(model.LastName),
            Age = model.Age,
            DateOfBirth = model.DateOfBirth,
            Gender = NormalizeRequired(model.Gender),
            Email = NormalizeRequired(model.Email),
            Phone = NormalizeRequired(model.Phone),
            Address = NormalizeRequired(model.Address),
            Username = NormalizeRequired(model.Username),
            Password = NormalizeRequired(model.Password),
            ProfileImagePath = model.ProfileImagePath,
            Qualifications = MapQualifications(model.Qualifications ?? new List<QualificationViewModel>()).ToList()
        };
    }

    private static IEnumerable<Qualification> MapQualifications(IEnumerable<QualificationViewModel> qualifications)
    {
        foreach (QualificationViewModel qualification in qualifications)
        {
            yield return new Qualification
            {
                CourseName = NormalizeRequired(qualification.CourseName),
                University = NormalizeRequired(qualification.University),
                PassingYear = qualification.PassingYear,
                Percentage = qualification.Percentage
            };
        }
    }

    private static void ValidateStudentModel(StudentRegistrationViewModel model)
    {
        EnsurePresent(model.FirstName, "First name");
        EnsurePresent(model.LastName, "Last name");
        EnsurePresent(model.Gender, "Gender");
        EnsurePresent(model.Email, "Email");
        EnsurePresent(model.Phone, "Phone");
        EnsurePresent(model.Address, "Address");
        EnsurePresent(model.Username, "Username");
        EnsurePresent(model.Password, "Password");

        if (!model.Age.HasValue)
        {
            throw new InvalidOperationException("Age is required.");
        }

        if (!model.DateOfBirth.HasValue)
        {
            throw new InvalidOperationException("Date of birth is required.");
        }

        foreach (QualificationViewModel qualification in model.Qualifications ?? new List<QualificationViewModel>())
        {
            EnsurePresent(qualification.CourseName, "Course name");
            EnsurePresent(qualification.University, "University");

            if (!qualification.PassingYear.HasValue)
            {
                throw new InvalidOperationException("Passing year is required.");
            }

            if (!qualification.Percentage.HasValue)
            {
                throw new InvalidOperationException("Percentage is required.");
            }
        }
    }

    private static void EnsurePresent(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"{fieldName} is required.");
        }
    }

    private static string NormalizeRequired(string? value)
    {
        return value?.Trim() ?? string.Empty;
    }
}
