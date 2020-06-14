using BugTracker.Data;
using BugTracker.Models.DomainModels;
using BugTracker.Models.ViewModels.Project;
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
        private readonly IUnitOfWork unitOfWork;

        public bool IsUserAuthorizedToAccessProject(int projectId)
        {
            var userId = GetUserId();
            return unitOfWork.Projects.IsUserInProject(projectId, userId);
        }

        public ProjectsController(IUnitOfWork unitOfWork) =>
            this.unitOfWork = unitOfWork;


        public string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        // GET: Projects
        public IActionResult Index()
        {
            var userId = GetUserId();
            var projectsRelatedToUser = unitOfWork.Users.GetProjectsRelatedToUser(userId);
            return View(projectsRelatedToUser);
        }

        // GET: Projects/Details/5
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return NotFound();
            if (!IsUserAuthorizedToAccessProject(id.Value)) return NotFound();
            var project = unitOfWork.Projects.Get(id.Value);
            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create() => View();

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Name")] CreateProjectViewModel createProjectViewModelproject)
        {
            if (!ModelState.IsValid) return View(createProjectViewModelproject);
            var projectToBeCreated = new Project
            {
                Name = createProjectViewModelproject.Name,
                DateCreated = DateTime.Now
            };
            unitOfWork.Projects.Add(projectToBeCreated);
            unitOfWork.Complete();
            unitOfWork.Projects.AddUserToProject(GetUserId(), projectToBeCreated.Id);
            unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        // GET: Projects/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {

            if (!id.HasValue) return NotFound();
            if (!IsUserAuthorizedToAccessProject(id.Value)) return NotFound();
            var project = unitOfWork.Projects.Get(id.Value);
            return View(project);

        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,DateCreated")] Project project)
        {
            if (id != project.Id) return NotFound();
            if (!IsUserAuthorizedToAccessProject(id)) return NotFound();
            if (!ModelState.IsValid) return View(project);

            var projectToUpdate = unitOfWork.Projects.Get(id);
            projectToUpdate.Name = project.Name;
            projectToUpdate.DateCreated = project.DateCreated;
            unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        // GET: Projects/Delete/5
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return NotFound();
            var project = unitOfWork.Projects.Get(id.Value);
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsUserAuthorizedToAccessProject(id)) return NotFound();
            var projectToBeDeleted = unitOfWork.Projects.Get(id);
            unitOfWork.Projects.Remove(projectToBeDeleted);
            unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

    }
}
