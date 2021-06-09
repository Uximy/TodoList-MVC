using System.Threading.Tasks;
using ITStep.Planner.Models;
using ITStep.Planner.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ITStep.Planner.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            _logger.LogInformation("Register view open");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequest register)
        {
            _logger.LogInformation("[POST] Register method start");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = register.Email,
                    UserName = register.Email
                };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(register);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            _logger.LogInformation("Login view open");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            _logger.LogInformation("[POST] Login method start");
            if (ModelState.IsValid)
            {
                //var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user is null)
                {
                    ModelState.AddModelError("", "Пользователь с такой почтой не зарегестрирован");
                    return View(login);
                }

                var result = await _userManager.CheckPasswordAsync(user, login.Password);
                if (result)
                {
                    await _signInManager.SignInAsync(user, login.RememberMe);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }

            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}