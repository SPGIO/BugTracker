using BugTracker.Models.Bugs;
using System;
using Xunit;

namespace BugTrackerXUnitTest
{
    public class BugTest
    {

        [Fact]
        public void Title_Unititialized_CanNotBeNull()
        {
            // Arrange
            var bugReport = new Bug();
            
            // Act

            // Assert
            Assert.NotNull(bugReport.Title);
        }

        [Fact]
        public void Title_SetToNull_ReturnsStringEmpty()
        {
            // Arrange
            var bugReport = new Bug();
            
            // Act
            bugReport.Title = null;
            string actual = bugReport.Title;
            
            // Assert
            Assert.NotNull(actual);
            Assert.Equal(string.Empty, actual);
        }


        [Fact]
        public void Status_Uninitialied_CanNotBeNull()
        { 
            // Arrange
            var bugReport = new Bug();
            
            // Act
            
            // Assert
            Assert.NotNull(bugReport.Status);
        }

        [Fact]
        public void Status_SetToNull_ReturnsCustomStatus()
        {
            // Arrange
            var bugReport = new Bug();
            
            // Act
            bugReport.Status = null;
            
            // Assert
            Assert.NotNull(bugReport.Status);
            Assert.Equal("Nothing selected", bugReport.Status.Name);
            Assert.Equal("#fff", bugReport.Status.Color);
            Assert.Equal(-1, bugReport.Status.Priority);
            Assert.Equal(-1, bugReport.Status.Id);
        }

        [Fact]
        public void Severity_Uninitialized_CanNotBeNull()
        {
            // Arrange
            var bugReport = new Bug();
            
            // Act
            
            // Assert
            Assert.NotNull(bugReport.Severity);
        }

        [Fact]
        public void Severity_SetToNull_ReturnsCustomSeverity()
        {
            // Arrange
            var bugReport = new Bug();
            
            // Act
            bugReport.Severity = null;
            
            // Assert
            Assert.NotNull(bugReport.Severity);
            Assert.Equal("Nothing selected", bugReport.Severity.Name);
            Assert.Equal("#fff", bugReport.Severity.Color);
            Assert.Equal(-1, bugReport.Severity.Priority);
            Assert.Equal(-1, bugReport.Severity.Id);
        }

        [Fact]
        public void Description_Uninitialized_CanNotBeNull()
        {
            // Arrange
            var bugReport = new Bug();
            
            // Act
            
            // Assert
            Assert.NotNull(bugReport.Description);
        }

        [Fact]
        public void Description_SetToNull_ReturnsStringEmpty()
        {
            // Arrange
            var bugReport = new Bug();
            
            // Act
            bugReport.Description = null;
            string actual = bugReport.Description;

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(string.Empty, actual);
        }


        [Fact]
        public void HowToReproduce_Uninitialized_CanNotBeNull()
        {
            // Arrange
            var bugReport = new Bug();
            
            // Act
            
            // Assert
            Assert.NotNull(bugReport.HowToReproduceBug);

           
        }

        [Fact]
        public void FixedBy_Uninitialized_IsNull()
        {
            // Arrange
            var bugReport = new Bug();
            
            // Act
            
            // Assert
            Assert.Null(bugReport.FixedBy);
        }

        [Fact]
        public void ReportedBy_Uninitialized_IsNull()
        {
            // Arrange
            var bugReport = new Bug();
            
            // Act
            
            // Assert
            Assert.Null(bugReport.ReportedBy);
        }

        [Fact]
        public void DateFixed_Uninitialized_IsNull()
        {
            // Arrange
            var bugReport = new Bug();
            
            // Act
            
            // Assert
            Assert.Null(bugReport.DateFixed);
        }



        [Fact]
        public void GetDaysFromToday_PassingDayInFuture_ReturnsZeroDays()
        {
            // Arrange
            var bugReport = new Bug();

            // Act
            DateTime tomorrow = DateTime.Now.AddDays(1);
            int actual = bugReport.GetDaysFromToday(tomorrow);

            // Assert
            Assert.Equal(0, actual);
        }

        [Fact]
        public void IsNew_BugReportJustCreated_ReturnsTrue()
        {
            // Arrange
            var bugReport = new Bug();

            // Act
            bool actual = bugReport.IsNew();

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void IsNew_CreatedMoreThanTwoDaysAgo_ReturnsFalse()
        {
            // Arrange
            var bugReport = new Bug();
            bugReport.DateReported = DateTime.Now.AddDays(-3);
            
            // Act
            bool actual = bugReport.IsNew();

            // Assert
            Assert.False(actual);
        }
    }
}
