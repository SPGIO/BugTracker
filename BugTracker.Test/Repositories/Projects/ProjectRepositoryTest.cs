using BugTracker.Data;
using BugTracker.Models;
using BugTracker.Models.Repositories.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Test.Repositories.Projects
{
    [TestClass]
    public class ProjectRepositoryTest
    {
        ApplicationDbContext _context;
        ProjectRepository _repository;

        [TestInitialize]
        public void init()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "aspnet-BugTracker-FCF27F9D-498E-4A6E-A676-9E509C78A655")
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new ProjectRepository(_context);
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
            var actual = true;
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
            var project1 = new Project() {Id = 1};
            var project2 = new Project() {Id = 2};
            _repository.Add(project1);
            _repository.Add(project2);

            await Assert.ThrowsExceptionAsync<Exception>(()
                => _repository.GetByIdAsync(0));
        }
    }
}
