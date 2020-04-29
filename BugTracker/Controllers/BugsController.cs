using BugTracker.Models;
using BugTracker.Models.Bugs;
using BugTracker.Models.Bugs.Severity;
using BugTracker.Models.Bugs.Status;
using BugTracker.Models.Repositories;
using BugTracker.Models.Repositories.Users;
using BugTracker.Models.Services.Bugs;
using BugTracker.Models.Services.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BugTracker.Controllers
{
    [Authorize]
    public class BugsController : Controller
    {
        private readonly IBugService bugService;
        private readonly IProjectService projectService;
        private readonly IRepository<BugStatus> statusRepository;
        private readonly IRepository<BugSeverity> severityRepository;
        private readonly IRepository<Project> projectRepository;
        private readonly IUserRepository userRepository;
        public BugsController(IRepository<Bug> bugRepository,
                              IRepository<BugStatus> statusRepository,
                              IRepository<BugSeverity> severityRepository,
                              IUserRepository userRepository,
                              IProjectService projectRepository)
        {

            bugService = new BugService(bugRepository);
            this.statusRepository = statusRepository;
            this.severityRepository = severityRepository;
            this.userRepository = userRepository;
            this.projectService = projectRepository;


        }

        public async Task<IProject> GetProjectRelatedToBug(IBug bug)
        {
            var allProjects = await projectRepository.GetAllAsync();
            return allProjects.First(project => project.Bugs.Contains(bug));
        }
        public string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        public async Task<bool> IsUserAuthorizedToAccessProject(int projectId)
        {
            var userId = GetUserId();
            var isUserInProject = await projectService
                .IsUserInProjectAsync(projectId, userId);
            return isUserInProject;
        }


        // GET: Bugs
        public async Task<IActionResult> Index(string ProjectName)
        {
            var project = await projectService.GetProjectByNameAsync(ProjectName);
            if (project == null) return NotFound();
            var isUserAuthorized = await IsUserAuthorizedToAccessProject(project.Id);
            if (!isUserAuthorized) return Unauthorized();
            return View(project.Bugs.OrderByDescending(bug => bug.DateReported));
        }

        // GET: Bugs/Details/5
        public async Task<IActionResult> Details(string ProjectName, int? id)
        {
            if (id == null) return NotFound();
            var project = await projectService.GetProjectByNameAsync(ProjectName);
            var isUserAuthorized = await IsUserAuthorizedToAccessProject(project.Id);
            if (!isUserAuthorized) return Unauthorized();
            var bugExist = await bugService.BugExistsAsync(id.Value);
            if (!bugExist) return NotFound();
            var bug = await bugService.GetBugByIdAsync(id.Value, false);
            return View(bug);
        }

        // GET: Bugs/Create
        public async Task<IActionResult> Create(string ProjectName)
        {
            var project = await projectService.GetProjectByNameAsync(ProjectName);
            var isUserAuthorized = await IsUserAuthorizedToAccessProject(project.Id);
            if (!isUserAuthorized) return Unauthorized();
            var severityList = await severityRepository.GetAllAsync();
            var selectItemSeverityList = severityList.Select(bugSeverity => new SelectListItem(bugSeverity.Name, bugSeverity.Id.ToString()));
            ViewBag.Severities = selectItemSeverityList;
            return View();
        }

        // POST: Bugs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string ProjectName, [Bind("Id,Title,Description,HowToReproduceBug,Severity,Status")] Bug bug)
        {
            try
            {
                var project = await projectService.GetProjectByNameAsync(ProjectName);
                bool projectExists = projectService.ProjectExists(project.Id);
                var isUserAuthorized = await IsUserAuthorizedToAccessProject(project.Id);
                if (!isUserAuthorized) return Unauthorized();
                if (!ModelState.IsValid) return View(bug);
                string userid = GetUserId();
                var user = await userRepository.GetById(userid);
                bug.ReportedBy = user;
                await bugService.AddBugAsync(bug);

                await projectService.AddBugReportToProject(project.Id, bug);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NoContent();
            }

        }

        // GET: Bugs/Edit/5
        public async Task<IActionResult> Edit(string ProjectName, int? id)
        {
            if (id == null) return NotFound();

            var project = await projectService.GetProjectByNameAsync(ProjectName);
            var isUserAuthorized = await IsUserAuthorizedToAccessProject(project.Id);
            if (!isUserAuthorized) return Unauthorized();

            if (await bugService.BugExistsAsync(id.Value) == false) return NotFound();
            var bug = await bugService.GetBugByIdAsync(id);
            if (bug == null) throw new Exception();
            var statusList = await statusRepository.GetAllAsync();
            var selectItemStatusList = statusList.Select(bugStatus => new SelectListItem(bugStatus.Name, bugStatus.Id.ToString(), bug.Status.Name == bugStatus.Name));
            ViewBag.Statuses = selectItemStatusList;


            var severityList = await severityRepository.GetAllAsync();
            var selectItemSeverityList = severityList.Select(bugSeverity => new SelectListItem(bugSeverity.Name, bugSeverity.Id.ToString(), bug.Severity.Name == bugSeverity.Name));
            selectItemSeverityList.First(severity => severity.Value == bug.Severity.Id.ToString()).Selected = true;
            ViewBag.Severities = selectItemSeverityList;

            return View(bug);
        }



        // POST: Bugs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string ProjectName, int id, [Bind("Id,Title,Description,HowToReproduceBug,DateReported,ReportedBy, Status, Severity, DateFixed,FixedBy")] Bug bug)
        {
            // , [Bind("Status")] int StatusId, [Bind("Severity")] int SeverityId
            var project = await projectService.GetProjectByNameAsync(ProjectName);

            var isUserAuthorized = await IsUserAuthorizedToAccessProject(project.Id);
            if (!isUserAuthorized) return Unauthorized();
            if (id != bug.Id) return NotFound();
            if (!ModelState.IsValid) return View(bug);
            bug.Severity = await severityRepository.GetByIdAsync(bug.Severity.Id);
            bug.Status = await statusRepository.GetByIdAsync(bug.Status.Id);
            try
            {
                await bugService.UpdateBugAsync(bug);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await bugService.BugExistsAsync(bug.Id)) throw;
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Bugs/Delete/5
        public async Task<IActionResult> Delete(string ProjectName, int? id)
        {
            if (id == null) return NotFound();
            var project = await projectService.GetProjectByNameAsync(ProjectName);
            var isUserAuthorized = await IsUserAuthorizedToAccessProject(project.Id);
            if (!isUserAuthorized) return Unauthorized();
            if (await bugService.BugExistsAsync(id.Value) == false) return NotFound();
            var bug = await bugService.GetBugByIdAsync(id, false);
            return View(bug);
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string ProjectName, int id)
        {
            var project = await projectService.GetProjectByNameAsync(ProjectName);
            var isUserAuthorized = await IsUserAuthorizedToAccessProject(project.Id);
            if (!isUserAuthorized) return Unauthorized();
            var bug = await bugService.GetBugByIdAsync(id);
            await bugService.DeleteBugAsync(bug);
            return RedirectToAction(nameof(Index));
        }
    }
}
