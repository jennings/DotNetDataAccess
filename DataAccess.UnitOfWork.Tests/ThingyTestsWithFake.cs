using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.UnitOfWork.App;
using DataAccess.UnitOfWork.Data;
using DataAccess.UnitOfWork.Tests.Support;
using Moq;
using NUnit.Framework;

namespace DataAccess.UnitOfWork.Tests
{
    [TestFixture]
    public class ThingyTestsWithFake
    {
        ICollection<Student> fakeBackingCollection;
        Mock<IStudentUnitOfWork> unitOfWorkWithFake;
        Thingy thingyWithFake;

        [SetUp]
        public void SetUp()
        {
            var fakeDbSet = new FakeRepository<Student>();
            fakeBackingCollection = fakeDbSet.BackingCollection;
            unitOfWorkWithFake = new Mock<IStudentUnitOfWork>();
            unitOfWorkWithFake.SetupGet(c => c.Students).Returns(fakeDbSet);
            thingyWithFake = new Thingy(unitOfWorkWithFake.Object);
        }

        [Test]
        public void UnitOfWork_DoWork_AddsAStudent_WithFakeRepository()
        {
            var startingCount = fakeBackingCollection.Count;
            thingyWithFake.DoWork();
            var endingCount = fakeBackingCollection.Count;
            Assert.AreEqual(startingCount + 1, endingCount);
        }
    }
}
