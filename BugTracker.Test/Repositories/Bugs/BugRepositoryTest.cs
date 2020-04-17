using BugTracker.Data;
using BugTracker.Models.Bugs;
using BugTracker.Models.Repositories.Bugs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugTracker.Test.Repositories.Bugs
{
    [TestClass]
    public class BugRepositoryTest
    {
        ApplicationDbContext _context;
        BugRepository _repository;
        
        [TestInitialize]
        public void init()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "aspnet-BugTracker-FCF27F9D-498E-4A6E-A676-9E509C78A655")
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new BugRepository(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
            _context = null;
            _repository = null;

        }

        [TestMethod]
        public void Add_passingNull_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(()
                => _repository.Add(null));
        }

        [TestMethod]
        public void Delete_PassingNull_ReturnFalse()
        {
            var actual = true;
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Update_PassingNull_ReturnFalse()
        {
            var actual =true;
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public async Task GetById_IdNotFoundInEmptyList_ThrowsKeyNotFoundException()
        {
            await Assert.ThrowsExceptionAsync<Exception>(()
                => _repository.GetByIdAsync(0));
        }

        [TestMethod]
        public async Task GetById_IdNotFound_ThrowsKeyNotFoundException()
        {
            var bug1 = new Bug() {Id = 1};
            var bug2 = new Bug() {Id = 2};
            _repository.Add(bug1);
            _repository.Add(bug2);

            await Assert.ThrowsExceptionAsync<Exception>(()
                => _repository.GetByIdAsync(0));
        }
    }
}
