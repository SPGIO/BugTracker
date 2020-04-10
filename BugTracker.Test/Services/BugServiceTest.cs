using BugTracker.Data;
using BugTracker.Models.Bugs;
using BugTracker.Models.Bugs.Priority;
using BugTracker.Models.Bugs.Status;
using BugTracker.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Test.Services
{
    [TestClass]
    public class BugServiceTest
    {

        

        [TestMethod]
        public async Task GetAll_DatabaseHasItems_ReturnsItems()
        {
            var criticalBugPriority = BugPriorityFactory.GetBugPriority(
                BugPriorityFactory.PriorityType.Critical);

            BugStatus fixedStatus = new FixedStatus() 
            { 
                Id = 1, 
                Name = "Fixed"
            };

            var stubData = (new List<Bug>
            {
                new Bug()
                {
                    Id = 1,
                    DateReported = new DateTime(2020,04,09),
                    DateFixed = new DateTime(2020,04,10),
                    HowToReproduceBug = "bug 2",
                    Priorty = criticalBugPriority,
                    Status = fixedStatus
            },
                new Bug()
                {
                    Id = 2,
                    DateReported = new DateTime(2020,04,09),
                    DateFixed = new DateTime(2020,04,10),
                    HowToReproduceBug = "bug 2",
                    Priorty = criticalBugPriority,
                    Status = fixedStatus
                }
            }).AsQueryable();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "")
                .Options;

            using(var context = new ApplicationDbContext(options))
            {
                context.AddRange(stubData);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var actual = await _bugService.GetAll();
            CollectionAssert.AreEqual(stubData.ToList(), actual.ToList());
            }

        }

        


        [TestMethod]
        public async Task AddContact_Given_contact_ExpectContactAdded()
        {
            BugPriority criticalBugPriority = BugPriorityFactory.GetBugPriority(
                BugPriorityFactory.PriorityType.Critical);


            BugStatus fixedStatus = new FixedStatus()
            {
                Id = 1,
                Name = "Fixed"
            };

            var bug = new Bug()
            {
                DateReported = new DateTime(2020, 04, 09),
                DateFixed = new DateTime(2020, 04, 10),
                HowToReproduceBug = "bug 2",
                Priorty = criticalBugPriority,
                Status = fixedStatus
            };
            const int expectedId = 1;
            _mockContext.Setup(x => x.SaveChanges()).Callback(() => bug.Id = expectedId);

            int id = await _bugService.Add(bug);

            _mockBugs.Verify(x => x.Add(bug), Times.Once);
            _mockContext.Verify(x => x.SaveChanges(), Times.Once);
            Assert.AreEqual(expectedId, id);
        }
    }
}
