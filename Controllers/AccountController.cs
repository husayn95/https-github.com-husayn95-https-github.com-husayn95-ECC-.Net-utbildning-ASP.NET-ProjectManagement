using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Models;
using ProjectManagementApp.Services;

namespace ProjectManagementApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _accountService.Login(model.Email, model.Password, model.RememberMe);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Project");
            }

            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _accountService.Register(model.FullName, model.Email, model.Password);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "En användare har skapats.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, result.ErrorMessage);
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _accountService.LogoutAsync();
            return RedirectToAction("Login");
        }


        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            var userEmail = User.Identity?.Name;

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login");
            }

            var user = _accountService.GetUserByEmail(userEmail);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new UserProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                Initials = GetInitials(user.FullName),
                StartedProjects = _accountService.GetStartedProjectsCount(user.Id.ToString()),
                CompletedTasks = _accountService.GetCompletedProjectsCount(user.Id.ToString()),
                RecentActivities = _accountService.GetRecentActivities(user.Id.ToString())
            };

            return View(model);
        }

        private string GetInitials(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return "";

            var names = fullName.Split(' ');
            if (names.Length == 1)
                return names[0][0].ToString().ToUpper();

            return (names[0][0].ToString() + names[1][0].ToString()).ToUpper();
        }

    }
}