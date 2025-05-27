using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Models;
using ProjectManagementApp.Services;

namespace ProjectManagementApp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IAccountService _accountService;

        public ProjectController(IProjectService projectService, IAccountService accountService)
        {
            _projectService = projectService;
            _accountService = accountService;


        }

        public async Task<IActionResult> Index(ProjectStatus? status = null)
        {
            IEnumerable<Project> projects;

            if (status.HasValue)
            {
                projects = await _projectService.GetProjectsByStatusAsync(status.Value);
                ViewData["CurrentStatus"] = status.Value;
            }
            else
            {
                projects = await _projectService.GetAllProjectsAsync();
                ViewData["CurrentStatus"] = null;
            }

            var userEmail = User.Identity?.Name;
            var currentUser = _accountService.GetUserByEmail(userEmail);
            ViewBag.UserFullName = currentUser?.FullName ?? "User";

            return View(projects);
        }

        public IActionResult Create()
        {

            var userEmail = User.Identity?.Name;
            var currentUser = _accountService.GetUserByEmail(userEmail);
            ViewBag.UserFullName = currentUser?.FullName ?? "User";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.AddProjectAsync(project);
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _projectService.UpdateProjectAsync(project);
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}