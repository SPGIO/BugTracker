using BugTracker.Data;
using BugTracker.Models.Bugs;
using BugTracker.Models.Bugs.Priority;
using BugTracker.Models.Bugs.Status;
using BugTracker.Services;
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

        private Mock<ApplicationDbContext> _mockBugContext;
        private Mock<DbSet<Bug>> _mockBugs;
        private BugService _bugService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockBugContext = new Mock<ApplicationDbContext>();
            _mockBugs = new Mock<DbSet<Bug>>();
            _mockBugContext.Setup(x => x.Bugs).Returns(_mockBugs.Object);
            _bugService = new BugService(_mockBugContext.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockBugContext.VerifyAll();
        }

        [TestMethod]
        public async Task GetAll_DatabaseHasItems_ReturnsItems()
        {
            BugPriority criticalBugPriority = BugPriorityFactory.GetBugPriority(
                BugPriorityFactory.PriorityType.Critical);


            BugStatus fixedStatus = new FixedStatus() 
            { 
                Id = 1, 
                Name = "Fixed"
            };

            var stubData = (new List<SimpleBug>
            {
                new SimpleBug()
                {
                    Id = 1,
                    DateReported = new DateTime(2020,04,09),
                    DateFixed = new DateTime(2020,04,10),
                    HowToReproduceBug = "bug 2",
                    Priorty = criticalBugPriority,
                    Status = fixedStatus
            },
                new SimpleBug()
                {
                    Id = 2,
                    DateReported = new DateTime(2020,04,09),
                    DateFixed = new DateTime(2020,04,10),
                    HowToReproduceBug = "bug 2",
                    Priorty = criticalBugPriority,
                    Status = fixedStatus
                }
            }).AsQueryable();

            SetupTestData(stubData, _mockBugs);

            var actual = await _bugService.GetAll();

            CollectionAssert.AreEqual(stubData.ToList(), actual.ToList());
        }

        private void SetupTestData<T>(IQueryable<T> testData, Mock<DbSet<T>> mockDbSet) where T : class
        {
            mockDbSet.As<IQueryable<IBug>>().Setup(m => m.Provider).Returns(testData.Provider);
            mockDbSet.As<IQueryable<IBug>>().Setup(m => m.Expression).Returns(testData.Expression);
            mockDbSet.As<IQueryable<IBug>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            mockDbSet.As<IQueryable<IBug>>().Setup(m => m.GetEnumerator())
                .Returns((IEnumerator<IBug>) testData.GetEnumerator());
        }

    }
}
