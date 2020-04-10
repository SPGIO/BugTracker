using BugTracker.Models.Bugs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BugTracker.Test.Repositories.Bugs
{
    [TestClass]
    public class TestBugComparer
    {
        [TestMethod]
        public void list1IsNull_returnsNotEqual()
        {
            var l1 = new List<Bug>();
            l1.Add(new Bug());
            var l2 = new List<Bug>();
            l2.Add(null);

            CollectionAssert.AreNotEqual(l1, l2, new BugComparer());
        }
        [TestMethod]
        public void list2IsNull_returnsNotEqual()
        {
            var l1 = new List<Bug>();
            l1.Add(null);
            var l2 = new List<Bug>();
            l2.Add(new Bug());

            CollectionAssert.AreNotEqual(l1, l2, new BugComparer());
        }

        [TestMethod]
        public void BothAreNull_returnEqual()
        {
            var l1 = new List<Bug>();
            l1.Add(null);
            var l2 = new List<Bug>();
            l2.Add(null);

            CollectionAssert.AreEqual(l1, l2, new BugComparer());
        }

        [TestMethod]
        public void BothEqual_ReturnsEqual()
        {
            var l1 = new List<Bug>();
            l1.Add(new Bug());
            var l2 = new List<Bug>();
            l2.Add(new Bug());
            CollectionAssert.AreEqual(l1, l2, new BugComparer());
        }

        [TestMethod]
        public void HowToReproduceBugIsDifferent_returnsNotEqual()
        {
            var l1 = new List<Bug>();
            l1.Add(new Bug()
            {
                Id = 1,
                HowToReproduceBug = "a"
            }) ;
            var l2 = new List<Bug>();
            l2.Add(new Bug()
            {
                Id = 1,
                HowToReproduceBug = "b"
            });
            CollectionAssert.AreEqual(l1, l2, new BugComparer());
        }

        [TestMethod]
        public void HowToReproduceBugIsAndIdAreEqual_returnsEqual()
        {
            var l1 = new List<Bug>();
            l1.Add(new Bug()
            {
                Id = 1,
                HowToReproduceBug = "a"
            });
            var l2 = new List<Bug>();
            l2.Add(new Bug()
            {
                Id = 1,
                HowToReproduceBug = "a"
            });
            CollectionAssert.AreEqual(l1, l2, new BugComparer());
        }
    }
}
