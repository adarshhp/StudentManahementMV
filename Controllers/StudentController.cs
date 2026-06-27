using AssignementProject.Interfaces.Services;
using AssignementProject.Models;
using AssignementProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AssignementProject.Controllers;

/// <summary>
/// Handles student registration, listing, details, edit, and delete screens.
/// </summary>
public class StudentController : Controller
{
    private readonly IStudentService _studentService;

    /// <summary>
    /// Initializes a new instance of the <see cref="StudentController"/> class.
    /// </summary>
    /// <param name="studentService">The student service.</param>
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>
    /// Displays all active students.
    /// </summary>
    /// <returns>The student list view.</returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            List<Student> students = await _studentService.GetAllStudentsAsync();
            return View(students);
        }
        catch
        {
            TempData["Error"] = "Unable to load students right now.";
            return View(new List<Student>());
        }
    }

    /// <summary>
    /// Displays the student registration form.
    /// </summary>
    /// <returns>The registration view.</returns>
    [HttpGet]
    public async Task<IActionResult> Register()
    {
        try
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            StudentRegistrationViewModel model = new()
            {
                StudentCode = await _studentService.GenerateStudentCodeAsync(),
                Qualifications = new List<QualificationViewModel>()
            };

            return View(model);
        }
        catch
        {
            TempData["Error"] = "Unable to open registration form right now.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>
    /// Registers a new student.
    /// </summary>
    /// <param name="model">The submitted student registration details.</param>
    /// <returns>The student list when successful; otherwise, the registration view.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(StudentRegistrationViewModel model)
    {
        try
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool isRegistered = await _studentService.RegisterStudentAsync(model);

            if (!isRegistered)
            {
                ModelState.AddModelError(string.Empty, "Student registration failed. Please try again.");
                return View(model);
            }

            TempData["Success"] = "Student registered successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
        catch
        {
            TempData["Error"] = "Unable to register student right now.";
            return View(model);
        }
    }

    /// <summary>
    /// Displays complete student details.
    /// </summary>
    /// <param name="id">The student primary key.</param>
    /// <returns>The student details view.</returns>
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            Student? student = await _studentService.GetStudentByIdAsync(id);

            if (student is null)
            {
                TempData["Error"] = "Student not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }
        catch
        {
            TempData["Error"] = "Unable to load student details right now.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>
    /// Displays the student edit form.
    /// </summary>
    /// <param name="id">The student primary key.</param>
    /// <returns>The edit view.</returns>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            Student? student = await _studentService.GetStudentByIdAsync(id);

            if (student is null)
            {
                TempData["Error"] = "Student not found.";
                return RedirectToAction(nameof(Index));
            }

            StudentRegistrationViewModel model = MapToRegistrationViewModel(student);
            return View(model);
        }
        catch
        {
            TempData["Error"] = "Unable to open edit form right now.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>
    /// Updates an existing student.
    /// </summary>
    /// <param name="model">The submitted student details.</param>
    /// <returns>The student list when successful; otherwise, the edit view.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(StudentRegistrationViewModel model)
    {
        try
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool isUpdated = await _studentService.UpdateStudentAsync(model);

            if (!isUpdated)
            {
                ModelState.AddModelError(string.Empty, "Student update failed. Please try again.");
                return View(model);
            }

            TempData["Success"] = "Student updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
        catch
        {
            TempData["Error"] = "Unable to update student right now.";
            return View(model);
        }
    }

    /// <summary>
    /// Soft deletes a student.
    /// </summary>
    /// <param name="id">The student primary key.</param>
    /// <returns>The student list page.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login", "Account");
            }

            bool isDeleted = await _studentService.DeleteStudentAsync(id);
            TempData[isDeleted ? "Success" : "Error"] = isDeleted
                ? "Student deleted successfully."
                : "Student delete failed. Please try again.";

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            TempData["Error"] = "Unable to delete student right now.";
            return RedirectToAction(nameof(Index));
        }
    }

    private bool IsUserLoggedIn()
    {
        return HttpContext.Session.GetInt32("UserId").HasValue;
    }

    private static StudentRegistrationViewModel MapToRegistrationViewModel(Student student)
    {
        return new StudentRegistrationViewModel
        {
            StudentId = student.StudentId,
            StudentCode = student.StudentCode,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Age = student.Age,
            DateOfBirth = student.DateOfBirth,
            Gender = student.Gender,
            Email = student.Email,
            Phone = student.Phone,
            Address = student.Address,
            Username = student.Username,
            Password = student.Password,
            ProfileImagePath = student.ProfileImagePath,
            Qualifications = student.Qualifications
                .Select(qualification => new QualificationViewModel
                {
                    CourseName = qualification.CourseName,
                    University = qualification.University,
                    PassingYear = qualification.PassingYear,
                    Percentage = qualification.Percentage
                })
                .ToList()
        };
    }
}
