using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StationeryStore.Mvc.ViewModels;
using StationeryStore.Mvc.Models;
using StationeryStore.Mvc.Services;

namespace StationeryStore.Mvc.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly ILogger<AccountController> _logger;
    private readonly IAuditLogService _auditService;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<AccountController> logger,
        IAuditLogService auditService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _auditService = auditService;

    }

    // ======================================================
    // REGISTER
    // ======================================================

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName,
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow
        };

        var result =
            await _userManager.CreateAsync(
                user,
                model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(
                user,
                "Staff");

            _logger.LogInformation(
                "New user registered. Email={Email}",
                model.Email);

            await _signInManager.SignInAsync(
                user,
                isPersistent: false);

            return RedirectToAction(
                "Index",
                "Home");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(
                string.Empty,
                error.Description);
        }

        _logger.LogWarning(
            "Register failed for {Email}. Errors={Errors}",
            model.Email,
            string.Join(", ",
                result.Errors.Select(x => x.Code)));

        return View(model);
    }

    // ======================================================
    // LOGIN
    // ======================================================

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(
        LoginViewModel model,
        string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
            return View(model);

        var result =
            await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true);

        if (result.Succeeded)
        {
            _logger.LogInformation(
                "User login success. Email={Email}",
                model.Email);

            await _auditService.LogAsync(
                "Login",
                "Account",
                null,
                "Success",
                model.Email);

            if (!string.IsNullOrWhiteSpace(returnUrl)
                && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Dashboard", "Stationery");
        }

        _logger.LogWarning(
            "Failed login attempt for {Email}",
            model.Email);

        ModelState.AddModelError(
            string.Empty,
            "Email hoặc mật khẩu không đúng.");

        return View(model);
    }

    // ======================================================
    // LOGOUT
    // ======================================================

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        _logger.LogInformation(
            "User logout. User={User}",
            User.Identity?.Name);
        
        await _auditService.LogAsync(
            "Logout",
            "Account",
            null,
            "Success",
            User.Identity?.Name);

        await _signInManager.SignOutAsync();

        return RedirectToAction(
            "Index",
            "Home");
    }

    // ======================================================
    // ACCESS DENIED
    // ======================================================

    [AllowAnonymous]
    public async Task<IActionResult> AccessDenied()
    {
        await _auditService.LogAsync(
            "AccessDenied",
            "Authorization",
            null,
            "Denied",
            User.Identity?.Name);
        return View();
    }
}