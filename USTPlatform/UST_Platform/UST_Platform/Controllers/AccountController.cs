using System.Security.Claims;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UST_Platform.Models;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager; // Define a field for the user manager service
        private readonly SignInManager<User> _signInManager; // Define a field for the sign in manager service 123456qQ!
        private readonly EmailService _emailService;
        //private readonly CourseService _courseService;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager; // Assign the user manager service to the field
            _signInManager = signInManager; // Assign the sign in manager service to the field
            _emailService = new EmailService(configuration);
            //_courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    ViewData["Title"] = "Email Confirmation";
                    return View();
                }
            }

            return View("AccessDenied");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            if (!User.Identity.IsAuthenticated)
            {
                var model = new RegistrationViewModel(); // Create a new instance of the RegistrationViewModel class

                ViewData["Title"] = "Register";

                return View(model); // Return the register view with the model
            }

            return RedirectToAction("Cabinet", new { username = User.Identity.Name });
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model) // Define an action method that accepts a RegistrationViewModel parameter
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = $"{model.LastName}_{model.FirstName}_{model.MiddleName}",
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                };
                var role = model.SelectedRole.ToString();

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role);
                    if (role == "Student")
                    {
                        await _userManager.AddClaimAsync(user, new Claim("Group", model.SelectedGroup.ToString()));
                    }
                    else
                    {
                        //if (_courseService.CheckCourseKey(model.SelectedGroup.ToString()))
                        //{
                        //    await _userManager.AddClaimAsync(user, new Claim("CourseKey", model.SelectedGroup.ToString()));
                        //}
                    }

                    // ДЛЯ ПЕРЕВІРКИ ПОШТИ ПОКИ ЗАЛИШИТИ ЗАКОМЕНТОВАНИМ (ліміт листів в місяць - 100)
                    //var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var confirmationURL = Url.Action(
                    //    "ConfirmEmail",
                    //    "Account",
                    //    values: new { userId = user.Id, token = confirmationToken });

                    //await _emailService.SendEmailAsync(
                    //    confirmationToken,
                    //    user.Email,
                    //    "Confirm your email",
                    //    "Your email adress was registered for UST Platform. " +
                    //    "Please, confirm your email adress or ignore this email if you did not registered. " +
                    //    $"Email confirmation link: <a href='https://localhost:7203{confirmationURL}'>confirm your email</a>");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Register", error.Description);
                    }

                    return View(model);
                }

                return RedirectToAction("Login");
            }

            return View(model); // Return the register view with the model and errors if any
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel(); // Create a new instance of the RegistrationViewModel class

            ViewData["Title"] = "Login";

            return View(model); // Return the register view with the model
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Cabinet", new { username = user.UserName });
                    }
                }

                ModelState.AddModelError("Login", "Failed to login.");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authorize]
        [Route("Account/Cabinet/{username}")]
        public async Task<IActionResult> Cabinet(string username)
        {
            if (username == User.Identity?.Name)
            {
                var user = await _userManager.FindByNameAsync(username);

                if (user != null)
                {
                    var userRole = await _userManager.GetRolesAsync(user);
                    var groupClaim = (await _userManager.GetClaimsAsync(user)).FirstOrDefault(u => u.Type == "Group");

                    ViewBag.User = user;
                    ViewBag.GroupClaim = groupClaim;
                    ViewBag.UserRole = userRole;

                    ViewData["Title"] = $"{user.UserName!.Replace('_', ' ')} Cabinet";

                    return View();
                }
            }

            return View("AccessDenied");
        }
    }
}
