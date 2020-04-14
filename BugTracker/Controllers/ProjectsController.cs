using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data;
using BugTracker.Models;
using BugTracker.Models.Services.Projects;
using BugTracker.Models.Repositories.Projects;
using System.Security.Claims;
using BugTracker.Models.Repositories.Users;

namespace BugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectService Service;
        private readonly IUserRepository userRepository;

        public ProjectsController(IProjectRepository repository, IUserRepository userRepository)
        {
            Service = new ProjectService(repository);
            this.userRepository = userRepository;
        }
        public string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
        public async Task<bool> IsUserAuthorizedToAccessProject(int projectId)
        {
            var userId = GetUserId();
            var isUserInProject = await Service.IsUserInProject(projectId, userId);
            return isUserInProject;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var allProjectsRelatedToUser = await Service.GetProjectsRelatedToUser(userId);
            return View(allProjectsRelatedToUser);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null) return NotFound();
                if (await IsUserAuthorizedToAccessProject(id.Value) == false)
                    return NoContent();
                var project = await Service.GetProjectById(id.Value);
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
                    var projectId = await Service.AddProject(project);
                    var userId = GetUserId();
                    await Service.AddTeamMember(projectId, userId);
                }
                catch (ProjectNotAddedException)
                {
                    throw;
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
                if (id == null) return NotFound();
                if (await IsUserAuthorizedToAccessProject(id.Value) == false)
                    return NoContent();
                var project = await Service.GetProjectById(id);
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
                    return NoContent();
                try
                {
                    await Service.UpdateProject(project);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await Service.ProjectExists(project.Id))
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
                    return NoContent();
                var project = await Service.GetProjectById(id);
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
            var project = await Service.GetProjectById(id);
            if (await IsUserAuthorizedToAccessProject(id) == false)
                return NoContent();

            await Service.DeleteProject(project);
            return RedirectToAction(nameof(Index));
        }

    }
}
