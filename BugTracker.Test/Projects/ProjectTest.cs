using BugTracker.Models;
using BugTracker.Models.Bugs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Test.Projects
{
    [TestClass]
    public class ProjectTest
    {
        [TestMethod]
        public void BugsListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var project = new Project();

            // Act


            // Assert
            Assert.AreEqual(0, project.Bugs.Count);
        }
        [TestMethod]
        public void BugsListsIsNotEmpty_ReturnsList()
        {
            // Arrange
            var project = new Project();

            // Act
            project.Bugs.Add(new Bug());

            // Assert
            Assert.AreEqual(1, project.Bugs.Count);
        }

        [TestMethod]
        public void BugsListIsNull_ReturnsEmptyList()
        {
            // Arrange
            var project = new Project();

            // Act
            project.Bugs = null;
           
            // Assert
            Assert.AreEqual(0, project.Bugs.Count);
        }

        [TestMethod]
        public void BugsListIsNull_ReturnsList()
        {
            // Arrange
            var project = new Project();

            // Act
            project.Bugs = null;
            project.Bugs.Add(new Bug());

            // Assert
            Assert.AreEqual(1, project.Bugs.Count);
        }
    }
}
