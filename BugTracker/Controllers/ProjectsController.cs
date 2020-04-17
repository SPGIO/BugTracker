using BugTracker.Models;
using BugTracker.Models.Repositories;
using BugTracker.Models.Repositories.Users;
using BugTracker.Models.Services.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public ProjectsController(IRepository<Project> projectRepository,
                                  IUserRepository userRepository)
        {
            _projectService = new ProjectService(projectRepository);
            this._userRepository = userRepository;
        }


        public string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
        public async Task<bool> IsUserAuthorizedToAccessProject(int projectId)
        {
            var userId = GetUserId();
            var isUserInProject = await _projectService.IsUserInProjectAsync(projectId, userId);
            return isUserInProject;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var allProjectsRelatedToUser = await _projectService.GetProjectsRelatedToUserAsync(userId);
            return View(allProjectsRelatedToUser);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (await IsUserAuthorizedToAccessProject(id.Value) == false)
                    return Unauthorized();
                var project = await _projectService.GetProjectByIdAsync(id.Value,false);
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
            if (ModelState.IsValid)
            {
                try
                {
                    var projectId = await _projectService.AddProjectAsync(project);
                    var userId = GetUserId();
                    var user = await _userRepository.GetById(userId);
                    await _projectService.AddUserToProjectAsync(projectId, user);
                }
                catch (ProjectNotAddedException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (await IsUserAuthorizedToAccessProject(id.Value) == false)
                    return Unauthorized();
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
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await IsUserAuthorizedToAccessProject(id) == false)
                    return Unauthorized();
                try
                {
                    await _projectService.UpdateProjectAsync(project);
                }
                catch (Exception)
                {
                    if (!await _projectService.ProjectExistsAsync(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null) return NotFound();
                if (await IsUserAuthorizedToAccessProject(id.Value) == false)
                    return Unauthorized();
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
            var project = await _projectService.GetProjectByIdAsync(id);
            if (await IsUserAuthorizedToAccessProject(id) == false)
                return Unauthorized();

            await _projectService.DeleteProjectAsync(project);
            return RedirectToAction(nameof(Index));
        }

    }
}
