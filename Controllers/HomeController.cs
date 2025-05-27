using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Models;
using ProjectManagementApp.Services;

namespace ProjectManagementApp.Controllers;

public class HomeController : Controller
{
    private readonly IProjectService _projectService;

    public HomeController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Index", "Project");
    }
}
