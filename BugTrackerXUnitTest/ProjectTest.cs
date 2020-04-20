using BugTracker.Models;
using BugTracker.Models.Bugs;
using System;
using System.Collections.Generic;
using Xunit;

namespace BugTrackerXUnitTest
{
    public class ProjectTest
    {
        [Fact]
        public void BugsListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var project = new Project();

            // Act
            var actual = project.Bugs;

            // Assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<ICollection<Bug>>(actual);
            Assert.Equal(0, actual.Count);
        }
        [Fact]
        public void BugsListsIsNotEmpty_ReturnsList()
        {
            // Arrange
            var project = new Project();

            // Act
            project.Bugs.Add(new Bug());
            var actual = project.Bugs;

            // Assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<ICollection<Bug>>(actual);
            Assert.Equal(1, actual.Count);
        }

        [Fact]
        public void BugsListIsNull_ReturnsEmptyList()
        {
            // Arrange
            var project = new Project();

            // Act
            project.Bugs = null;
            var actual =  project.Bugs;
            // Assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<ICollection<Bug>>(actual);
            Assert.Equal(0, actual.Count);
        }

        [Fact]
        public void BugsListIsNull_ReturnsList()
        {
            // Arrange
            var project = new Project();

            // Act
            project.Bugs = null;
            project.Bugs.Add(new Bug());
            var actual = project.Bugs;

            // Assert
            Assert.NotNull(actual);
            Assert.IsAssignableFrom<ICollection<Bug>>(actual);
            Assert.Equal(1, actual.Count);
        }

        
    }
}
