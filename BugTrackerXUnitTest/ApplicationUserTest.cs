using BugTracker.Models.DomainModels;
using BugTracker.Models.Users;
using System.Collections.Generic;
using Xunit;

namespace BugTrackerXUnitTest
{

    public class ApplicationUserTest
    {
        [Fact]
        public void Projects_ReturnsEmptyListDefault()
        {
            // Arrange
            var user = new ApplicationUser();

            // Act
            var actual = user.Projects;

            // Assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<ICollection<UserProjects>>(actual);
            Assert.Equal(0, actual.Count);
        }


        [Fact]
        public void Projects_returnsEmptyListIfProjectsIsNull()
        {
            // Arrange
            var user = new ApplicationUser();

            // Act
            user.Projects = null;
            var actual = user.Projects;

            // Assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<ICollection<UserProjects>>(actual);
            Assert.Equal(0, actual.Count);
        }

        [Fact]
        public void Projects_returnsCorrectNumberOfElements_IfNullAndProjectIsAdded()
        {
            // Arrange
            var user = new ApplicationUser();

            // Act
            user.Projects = null;
            user.Projects.Add(new UserProjects());
            var actual = user.Projects;

            // Assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<ICollection<UserProjects>>(actual);
            Assert.Equal(1, actual.Count);
        }
    }
}
