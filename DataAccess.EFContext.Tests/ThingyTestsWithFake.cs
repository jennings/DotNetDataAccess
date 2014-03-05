using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EFContext.App;
using DataAccess.EFContext.Data;
using DataAccess.EFContext.Tests.Support;
using Moq;
using NUnit.Framework;

namespace DataAccess.EFContext.Tests
{
    [TestFixture]
    public class ThingyTestsWithFake
    {
        ICollection<Student> fakeBackingCollection;
        Mock<StudentContext> contextWithFake;
        Thingy thingyWithFake;

        [SetUp]
        public void SetUp()
        {
            var fakeDbSet = new FakeDbSet<Student>();
            fakeBackingCollection = fakeDbSet.BackingCollection;
            contextWithFake = new Mock<StudentContext>();
            contextWithFake.SetupGet(c => c.Students).Returns(fakeDbSet);
            thingyWithFake = new Thingy(contextWithFake.Object);
        }

        [Test]
        public void EFContext_DoWork_AddsAStudent_WithFakeDbSet()
        {
            var startingCount = fakeBackingCollection.Count;
            thingyWithFake.DoWork();
            var endingCount = fakeBackingCollection.Count;
            Assert.AreEqual(startingCount + 1, endingCount);
        }
    }
}
