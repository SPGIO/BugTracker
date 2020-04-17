using BugTracker.Controllers;
using BugTracker.Data;
using BugTracker.Models;
using BugTracker.Models.Repositories;
using BugTracker.Models.Repositories.Projects;
using BugTracker.Models.Repositories.Users;
using BugTracker.Models.Services.Projects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace BugTrackerXUnitTest
{
    public class ProjectControllerTest
    {
        private ClaimsPrincipal GetTestUser() 
            => new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, "SomeValueHere"),
                new Claim(ClaimTypes.Name, "gunnar@somecompany.com")
                // other required and custom claims
                }, "TestAuthentication"));

        [Fact]
        public async Task Index_ActionExecutes_ReturnsViewForIndex()
        {
            // Arrange
            var repo = new Mock<IRepository<Project>>();
            var userRepo = new Mock<IUserRepository>();
            var controller = new ProjectsController(repo.Object, userRepo.Object);
            var testUser = GetTestUser();
            controller.ControllerContext.HttpContext = 
                new DefaultHttpContext { User = testUser };

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Index_ActionExecutes_ReturnsExactNumberOfEmployees()
        {
            // Arrange
            var mockProjectRepository = new Mock<IRepository<Project>>();
            var mockService = new Mock<IProjectService>();
            var mockUserRepository = new Mock<IUserRepository>();
            var controller = new ProjectsController(mockProjectRepository.Object, mockUserRepository.Object);
            var testUser = GetTestUser();
            controller.ControllerContext.HttpContext =
                new DefaultHttpContext { User = testUser };

            var projectsStub = new List<Project>()
            {
                new Project(),
                new Project()
            };
            mockService
                .Setup(repo => repo.GetAllProjectsAsync())
                .ReturnsAsync(projectsStub);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var projects = Assert.IsType<List<Project>>(viewResult);
            Assert.Equal(2, projects.Count);
        
        }
    }
}
