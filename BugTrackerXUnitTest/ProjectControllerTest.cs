using BugTracker.Models.DomainModels;
using BugTracker.Models.Services.Projects;
using BugTracker.Models.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BugTrackerXUnitTest
{
    public class ProjectControllerTest : IDisposable
    {
        Mock<IProjectService> mockService;
        public ProjectControllerTest()
        {
            mockService = new Mock<IProjectService>();
        }

        public void Dispose()
        {
            mockService.VerifyAll();
        }

        public List<Project> GetProjectStub()
        {
            var userId = "1234";
            var username = "Test User";
            var user = new ApplicationUser();
            user.Id = userId;
            user.UserName = username;

            var project1 = new Project(){ Id = 1 };
            var projectRelation1 = new UserProjects()
            {
                ProjectId = 1,
                Project = project1,
                UserId = userId,
                User = user
            };
            project1.Team.Add(projectRelation1);
            user.Projects.Add(projectRelation1);

            var project2 = new Project(){ Id = 2 };
            var projectRelation2 = new UserProjects()
            {
                ProjectId = 12,
                Project = project2,
                UserId = userId,
                User = user
            };
            project2.Team.Add(projectRelation2);
            user.Projects.Add(projectRelation2);



            var userAndProjectRelation = new List<UserProjects>();
            userAndProjectRelation.Add(projectRelation2);

            var projectList = new List<Project>();
            projectList.Add(project1);
            projectList.Add(project2);

            return projectList;
        }


        private ClaimsPrincipal GetTestUser(string id, string name)
            => new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Name, name)
                // other required and custom claims
            }, "TestAuthentication"));

        [Fact]
        public async Task Index_ActionExecutes_ReturnsViewForIndex()
        {
            //// Arrange
            //var userRepo = new Mock<IUserRepository>();
            //var controller = new ProjectsController(mockService.Object, userRepo.Object);

            //string userId = "1234";
            //string username = "Test dummy";
            //var testUser = GetTestUser(userId, username);
            //controller.ControllerContext.HttpContext =
            //    new DefaultHttpContext { User = testUser };

            //// Act
            //var result = await controller.Index();

            //// Assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<Project>>(
            //    viewResult.ViewData.Model);
            //Assert.Empty(model);
        }

        [Fact]
        public async Task Index_ActionExecutes_ReturnsExactNumberOfProjects()
        {
            //// Arrange
            //var mockUserRepository = new Mock<IUserRepository>();
            //var controller = new ProjectsController(mockService.Object, mockUserRepository.Object);

            //string userId = "1234";
            //string username = "Test dummy";
            //var testUser = GetTestUser(userId, username);
            //controller.ControllerContext.HttpContext =
            //    new DefaultHttpContext { User = testUser };

            //var projectsStub = GetProjectStub();
            //mockService
            //    .Setup(service => service.GetProjectsRelatedToUserAsync(userId))
            //    .ReturnsAsync(projectsStub);

            //// Act
            //var result = await controller.Index();

            //// Assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //var projects = Assert.IsAssignableFrom<IEnumerable<Project>>(
            //    viewResult.ViewData.Model);
            //Assert.Equal(2, projects.Count());
            //mockService.Verify();
        }

        [Fact]
        public async Task Index_ActionExecutes_NotLoggedIn()
        {
            //// Arrange
            //var mockUserRepository = new Mock<IUserRepository>();
            //var controller = new ProjectsController(mockService.Object, mockUserRepository.Object);
            //string userId = "1234";
            //string username = "Test dummy";
            //var testUser = GetTestUser(userId, username);
            //controller.ControllerContext.HttpContext =
            //    new DefaultHttpContext { User = testUser };

            //var projectsStub = GetProjectStub();
            //mockService
            //    .Setup(service => service.GetProjectsRelatedToUserAsync(userId))
            //    .ReturnsAsync(projectsStub);

            //// Act
            //var result = await controller.Index();

            //// Assert
            //Assert.IsType<ViewResult>(result);
        }



        [Fact]
        public async Task Details_ActionExecutes_IdIsNull()
        {
            //// Arrange
            //var mockUserRepository = new Mock<IUserRepository>();
            //var controller = new ProjectsController(mockService.Object, mockUserRepository.Object);

            //string userId = "1234";
            //string username = "Test dummy";
            //var testUser = GetTestUser(userId, username);
            //controller.ControllerContext.HttpContext =
            //        new DefaultHttpContext
            //        {
            //            User = testUser
            //        };

            //var projectsStub = GetProjectStub();
            //mockService
            //    .Setup(service => service.GetProjectsRelatedToUserAsync(userId))
            //    .ReturnsAsync(projectsStub);

            //// Act
            //var result = await controller.Index();

            //// Assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //var projects = Assert.IsAssignableFrom<IEnumerable<Project>>(
            //    viewResult.ViewData.Model);
            //Assert.Equal(2, projects.Count());
        }

    }
}
