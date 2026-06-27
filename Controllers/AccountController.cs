using AssignementProject.Interfaces.Services;
using AssignementProject.Models;
using AssignementProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AssignementProject.Controllers;

/// <summary>
/// Handles login and logout operations for the application.
/// </summary>
public class AccountController : Controller
{
    private readonly ILoginService _loginService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    /// <param name="loginService">The login service.</param>
    public AccountController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    /// <summary>
    /// Displays the login page.
    /// </summary>
    /// <returns>The login view.</returns>
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// Validates login credentials and starts a simple session when successful.
    /// </summary>
    /// <param name="model">The submitted login details.</param>
    /// <returns>The login view when invalid; otherwise, the student list page.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Student? student = await _loginService.ValidateLoginAsync(model);

            if (student is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", student.StudentId);
            HttpContext.Session.SetString("Username", student.Username ?? string.Empty);

            return RedirectToAction("Index", "Student");
        }
        catch
        {
            TempData["Error"] = "Unable to process login right now. Please try again.";
            return View(model);
        }
    }

    /// <summary>
    /// Clears the current user session and redirects to login.
    /// </summary>
    /// <returns>The login page.</returns>
    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }
}
