using BugTracker.Data;
using BugTracker.Models.DomainModels;
using BugTracker.Models.ViewModels.Bugs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BugTracker.Controllers
{


    [Authorize]
    public class BugsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public BugsController(IUnitOfWork unitOfWork) : base() =>
            this.unitOfWork = unitOfWork;

        public string GetUserId() =>
           User.FindFirstValue(ClaimTypes.NameIdentifier);

        public Project GetProjectFromRouteData()
        {
            string projectName = (string) RouteData.Values["ProjectName"];
            return unitOfWork.Projects.GetByName(projectName);
        }

        public bool CanGetProjectId()
        {
            var project = GetProjectFromRouteData();
            return project != null;
        }

        public bool TryGetProjectId(out int projectId)
        {
            var project = GetProjectFromRouteData();
            projectId = project == null ? 0 : project.Id;
            return project != null;
        }
        public int GetProjectId() => GetProjectFromRouteData().Id;

        public bool IsUserAuthorizedToAccessProject()
        {
            int projectId;
            if (!TryGetProjectId(out projectId)) return false;
            var userId = GetUserId();
            return unitOfWork.Projects.IsUserInProject(projectId, userId);
        }









        private void PopulateViewBagWithSeverityList()
        {
            var severityList = unitOfWork.Severities
                .GetAll()
                .Select(severity => new SelectListItem(severity.Name, severity.Id.ToString()));
            ViewBag.Severities = severityList;
        }


        // GET: Bugs
        public IActionResult Index()
        {
            if (!IsUserAuthorizedToAccessProject()) return NotFound();

            var reports = unitOfWork.BugReports.GetByProjectId(GetProjectId())
                .Select(report => new ListBugReportViewModel
                {
                    DateReported = report.DateReported,
                    Description = report.Description,
                    Id = report.Id,
                    IsNew = report.IsNew(),
                    ReportedByName = report.ReportedBy.UserName,
                    SeverityColor = report.Severity.Color,
                    SeverityName = report.Severity.Name,
                    StatusColor = report.Status.Color,
                    StatusName = report.Status.Name,
                    Title = report.Title
                }).OrderByDescending(reports => reports.DateReported);

            return View(reports);
        }

        // GET: Bugs/Details/5
        public IActionResult Details(int? id)
        {
            if (!IsUserAuthorizedToAccessProject() && !id.HasValue) return NotFound();
            var report = unitOfWork.BugReports.Get(id.Value);
            if (report != null) return View(report);
            return NotFound();
        }

        // GET: Bugs/Create
        public IActionResult Create()
        {
            if (!IsUserAuthorizedToAccessProject()) return NotFound();
            PopulateViewBagWithSeverityList();
            return View();

        }

        // POST: Bugs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,HowToReproduce,SeverityId")] CreateBugReportViewModel createBugReportViewModel)
        {
            if (!IsUserAuthorizedToAccessProject()) return NotFound();

            if (!ModelState.IsValid)
            {
                PopulateViewBagWithSeverityList();
                return View(createBugReportViewModel);
            }

            var user = unitOfWork.Users.GetByStringId(GetUserId());
            int OpenStatusId = 3; // This value is from the DB

            var newBugReport = new BugReport()
            {
                Title = createBugReportViewModel.Title,
                DateFixed = null,
                DateReported = DateTime.Now,
                Description = createBugReportViewModel.Description,
                FixedBy = null,
                HowToReproduceBug = createBugReportViewModel.HowToReproduce,
                ReportedBy = user,
                Severity = unitOfWork.Severities.Get(createBugReportViewModel.SeverityId),
                Status = unitOfWork.Status.Get(OpenStatusId)
            };

            unitOfWork.BugReports.Add(newBugReport);
            var project = unitOfWork.Projects.Get(GetProjectId());
            project.Bugs.Add(newBugReport);
            unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }



        // GET: Bugs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (!IsUserAuthorizedToAccessProject() && !id.HasValue) return NotFound();

            var report = unitOfWork.BugReports.GetWithSeverityAndStatus(id.Value);
            
            var statusList = unitOfWork.Status.GetAll();
            ViewBag.Statuses = ConvertToStatusList(report, statusList);
            
            var severityList = unitOfWork.Severities.GetAll();
            ViewBag.Severities = ConvertToSeverityList(report, severityList);

            var editViewModel = new EditBugReportViewModel
            {
                Description = report.Description,
                HowToReproduce = report.HowToReproduceBug,
                Id = report.Id,
                SeverityId = report.Severity.Id,
                StatusId = report.Status.Id,
                Title = report.Title
            };
            return View(editViewModel);
        }

        private static IEnumerable<SelectListItem> ConvertToSeverityList(BugReport report, IEnumerable<BugReportSeverity> severityList)
        {
            return severityList.Select(severity =>
                    new SelectListItem(severity.Name,
                                       severity.Id.ToString(),
                                       report.Severity.Name == severity.Name));
        }

        private IEnumerable<SelectListItem> ConvertToStatusList(BugReport report, IEnumerable<BugReportStatus> statusList)
        {
            var returnlist = statusList.Select(status => new SelectListItem(status.Name,
                                                   status.Id.ToString(),
                                                   report.Status.Name == status.Name));
            
            // Set the selected value equal to the value from DB
            returnlist.First(severity => severity.Value == report.Severity.Id.ToString()).Selected = true; ;
            return returnlist;
        }



        // POST: Bugs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Description,HowToReproduce, StatusId, SeverityId")] EditBugReportViewModel editBugReportViewModel)
        {
            if (!IsUserAuthorizedToAccessProject()) return NotFound();
            if (id != editBugReportViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(editBugReportViewModel);
           
            var bugReportToBeEdited = unitOfWork.BugReports.Get(id);
            var selectedSeverity = unitOfWork.Severities.Get(editBugReportViewModel.SeverityId);
            var selectedStatus = unitOfWork.Status.Get(editBugReportViewModel.StatusId);

            bugReportToBeEdited.Description = editBugReportViewModel.Description;
            bugReportToBeEdited.HowToReproduceBug = editBugReportViewModel.HowToReproduce;
            bugReportToBeEdited.Severity = selectedSeverity;
            bugReportToBeEdited.Description = editBugReportViewModel.Description;
            bugReportToBeEdited.Title = editBugReportViewModel.Title;
            bugReportToBeEdited.Severity = selectedSeverity;
            bugReportToBeEdited.Status = selectedStatus;

            unitOfWork.Complete();

            return RedirectToAction(nameof(Index));


        }

        // GET: Bugs/Delete/5
        public IActionResult Delete(int? id)
        {
            if (!IsUserAuthorizedToAccessProject() && !id.HasValue) return Unauthorized();
            var report = unitOfWork.BugReports.Get(id.Value);
            return View(report);
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsUserAuthorizedToAccessProject()) return NotFound();
            var report = unitOfWork.BugReports.Get(id);
            unitOfWork.BugReports.Remove(report);
            unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
    }
}
