using BugTracker.Models;
using BugTracker.Models.Repositories.Users;
using BugTracker.Models.Services.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BugTracker.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IUserRepository _userRepository;

        public ProjectsController(IProjectService projectService,
                                  IUserRepository userRepository)
        {
            this._projectService = projectService;
            this._userRepository = userRepository;
        }


        public string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
        public async Task<bool> IsUserNotAuthorizedToViewProject(int projectId)
        {
            var userId = GetUserId();
            var isUserInProject = await _projectService.IsUserInProjectAsync(projectId, userId);
            return !isUserInProject;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var projectsRelatedToUser = await _projectService
                .GetProjectsRelatedToUserAsync(userId);
            return View(projectsRelatedToUser);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await IsUserNotAuthorizedToViewProject(id.Value)) return Unauthorized();
            try
            {
                var project = await _projectService.GetProjectByIdAsync(id.Value);
                return View(project);
            }
            catch (ProjectNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateCreated")] Project project)
        {
            try
            {
                if (!ModelState.IsValid) return View(project);
                var projectId = await _projectService.AddProjectAsync(project);
                var userId = GetUserId();
                await _projectService.AddUserToProjectAsync(projectId, userId);
                return RedirectToAction(nameof(Index));
            }
            catch (ProjectNotAddedException)
            {
                return NotFound();
            }


        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await IsUserNotAuthorizedToViewProject(id.Value)) return Unauthorized();
            try
            {
                var project = await _projectService.GetProjectByIdAsync(id);
                return View(project);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateCreated")] Project project)
        {
            if (await IsUserNotAuthorizedToViewProject(id)) return Unauthorized();
            if (id != project.Id) return NotFound();
            if (!ModelState.IsValid) return View(project);
            try
            {
                await _projectService.UpdateProjectAsync(project);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }


        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await IsUserNotAuthorizedToViewProject(id.Value)) return Unauthorized();
            try
            {
                var project = await _projectService.GetProjectByIdAsync(id);
                return View(project);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await IsUserNotAuthorizedToViewProject(id)) return Unauthorized();
            var project = await _projectService.GetProjectByIdAsync(id);
            await _projectService.DeleteProjectAsync(project);
            return RedirectToAction(nameof(Index));
        }

    }
}
