using BugTracker.Models;
using BugTracker.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Test.Users
{
    [TestClass]
    public class ApplicationUserTest
    {
        [TestMethod]
        public void Projects_ReturnsEmptyListDefault()
        {
            // Arrange
            var user = new ApplicationUser();

            // Act
            var actual = user.Projects;

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ICollection<UserProjects>));
            Assert.AreEqual(0, actual.Count);
        }


        [TestMethod]
        public void Projects_returnsEmptyListIfProjectsIsNull()
        {
            // Arrange
            var user = new ApplicationUser();

            // Act
            user.Projects = null;
            var actual = user.Projects;

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ICollection<UserProjects>));
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void Projects_returnsCorrectNumberOfElements_IfNullAndProjectIsAdded()
        {
            // Arrange
            var user = new ApplicationUser();

            // Act
            user.Projects = null;
            user.Projects.Add(new UserProjects());
            var actual = user.Projects;

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(ICollection<UserProjects>));
            Assert.AreEqual(1, actual.Count);
        }
    }
}
