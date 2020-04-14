using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data;
using BugTracker.Models.Bugs;
using BugTracker.Models.Services.Bugs;
using BugTracker.Models.Repositories.Bugs;
using BugTracker.Models.Repositories.Severity;
using BugTracker.Models.Repositories.Status;
using BugTracker.Models.Bugs.Severity;
using BugTracker.Models.Bugs.Status;
using System.Security.Claims;
using BugTracker.Models.Repositories.Users;
using BugTracker.Models.Repositories.Projects;
using BugTracker.Models;
using BugTracker.Models.Services.Projects;

namespace BugTracker.Controllers
{
    public class BugsController : Controller
    {
        private readonly IBugService bugService;
        private readonly IStatusRepository statusRepository;
        private readonly ISeverityRepository severityRepository;
        private readonly IUserRepository userRepository;
        private readonly IProjectRepository projectRepository;
        private readonly ProjectService projectService;
        private  Project Project;
        public BugsController(IBugRepository bugRepository,
                              IStatusRepository statusRepository,
                              ISeverityRepository severityRepository,
                              IUserRepository userRepository,
                              IProjectRepository projectRepository)
        {

            bugService = new BugService(bugRepository);
            this.statusRepository = statusRepository;
            this.severityRepository = severityRepository;
            this.userRepository = userRepository;
            this.projectRepository = projectRepository;
            this.projectService = new ProjectService(projectRepository);
            
            
        }

        public async Task<Project> GetProjectRelatedToBug(Bug bug)
        {
            var allProjects = await projectRepository.GetAll();
            return allProjects.First(project => project.Bugs.Contains(bug));
        }
        public string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
        public async Task<bool> IsUserInProject(string projectName)
        {
            if (projectName == null) return false;
            var userId = GetUserId();
            var user = await userRepository.GetById(userId);
            var allProjects = await projectRepository.GetAll();
            var project = allProjects.FirstOrDefault(project 
                                                => project.Name == projectName);
            return project.Team.Contains(user);
        }


        // GET: Bugs
        public async Task<IActionResult> Index(string ProjectName)
        {
            if (!await IsUserInProject(ProjectName)) return NoContent();
            IEnumerable<Project> allProjects = projectRepository.GetAll().Result;
            Project = allProjects.FirstOrDefault(project => project.Name == ProjectName);
            if (Project == null) return NotFound();
            return View(Project.Bugs);
        }

        // GET: Bugs/Details/5
        public async Task<IActionResult> Details(string ProjectName, int? id)
        {
            if (!await IsUserInProject(ProjectName)) return NoContent();
            if (id == null) return NotFound();
            if (await bugService.BugExists(id.Value) == false) return NotFound();
            var bug = await bugService.GetBugById(id.Value);
            return View(bug);
        }

        // GET: Bugs/Create
        public async Task<IActionResult> Create(string ProjectName)
        {
            if (!await IsUserInProject(ProjectName)) return NoContent();
            var severityList = await severityRepository.GetAll();
            var selectItemSeverityList = severityList.Select(bugSeverity => new SelectListItem(bugSeverity.Name, bugSeverity.Id.ToString()));
            ViewBag.Severities = selectItemSeverityList;
            return View();
        }

        // POST: Bugs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string ProjectName, [Bind("Id,Title,Description,HowToReproduceBug,DateReported,ReportedBy,Severity,Status")] Bug bug)
        {
            if (!await IsUserInProject(ProjectName)) return NoContent();
            if (!ModelState.IsValid) return View(bug);

            try
            {
                var severityIdString = Request.Form["Severity"].ToString();
                var severityId = int.Parse(severityIdString);
                var severity = await severityRepository.GetById(severityId);

                var statuses = await statusRepository.GetAll();
                var status = statuses.First(status => status.Name.ToLower() == "open");
                bug.Severity = severity;
                bug.Status = status;

                string userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = await userRepository.GetById(userid);
                bug.ReportedBy = user;

                await bugService.AddBug(bug);
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
            var statusList = await statusRepository.GetAll();
            var selectItemStatusList = statusList.Select(bugStatus => new SelectListItem(bugStatus.Name, bugStatus.Id.ToString()));
            ViewBag.Statuses = selectItemStatusList;

            var severityList = await severityRepository.GetAll();
            var selectItemSeverityList = severityList.Select(bugSeverity => new SelectListItem(bugSeverity.Name, bugSeverity.Id.ToString()));
            ViewBag.Severities = selectItemSeverityList;

            if (id == null) return NotFound();
            if (await bugService.BugExists(id.Value) == false) return NotFound();
            var bug = await bugService.GetBugById(id);
            return View(bug);
        }



        // POST: Bugs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string ProjectName, int id, [Bind("Id,Title,Description,HowToReproduceBug,DateReported,ReportedBy,DateFixed,FixedBy")] Bug bug)
        {
            if (!await IsUserInProject(ProjectName)) return NoContent();
            if (id != bug.Id) return NotFound();
            if (!ModelState.IsValid) return View(bug);
            try
            {
                var severityIdString = Request.Form["Severity"].ToString();
                var severityId = int.Parse(severityIdString);
                var severity = await severityRepository.GetById(severityId);

                var statusIdString = Request.Form["Status"].ToString();
                var statusId = int.Parse(statusIdString);
                var status = await statusRepository.GetById(statusId);

                bug.Severity = severity;
                bug.Status = status;
                await bugService.UpdateBug(bug);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await bugService.BugExists(bug.Id)) throw;
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Bugs/Delete/5
        public async Task<IActionResult> Delete(string ProjectName, int? id)
        {
            if (!await IsUserInProject(ProjectName)) return NoContent();
            if (id == null) return NotFound();
            if (await bugService.BugExists(id.Value) == false) return NotFound();
            var bug = await bugService.GetBugById(id);
            return View(bug);
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string ProjectName, int id)
        {
            if (!await IsUserInProject(ProjectName)) return NoContent();
            var bug = await bugService.GetBugById(id);
            await bugService.DeleteBug(bug);
            return RedirectToAction(nameof(Index));
        }
    }
}
