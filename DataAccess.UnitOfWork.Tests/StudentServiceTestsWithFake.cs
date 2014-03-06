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
    public class StudentServiceTestsWithFake
    {
        ICollection<Student> fakeBackingCollection;
        Mock<IStudentUnitOfWork> unitOfWorkWithFake;
        StudentService serviceWithFake;

        [SetUp]
        public void SetUp()
        {
            var fakeDbSet = new FakeRepository<Student>();
            fakeBackingCollection = fakeDbSet.BackingCollection;
            unitOfWorkWithFake = new Mock<IStudentUnitOfWork>();
            unitOfWorkWithFake.SetupGet(c => c.Students).Returns(fakeDbSet);
            serviceWithFake = new StudentService(unitOfWorkWithFake.Object);
        }

        [Test]
        public void UnitOfWork_WithFake_CreateStudents_AddsStudents()
        {
            var startingCount = fakeBackingCollection.Count;
            serviceWithFake.CreateStudents();
            var endingCount = fakeBackingCollection.Count;
            Assert.Greater(endingCount, startingCount);
        }

        [Test]
        public void UnitOfWork_WithFake_PromoteAllClasses_IncrementsYears()
        {
            var student = new Student { YearsCompleted = 2, IsGraduated = false };
            fakeBackingCollection.Add(student);

            serviceWithFake.PromoteAllClasses();

            Assert.AreEqual(3, student.YearsCompleted);
        }

        [Test]
        public void UnitOfWork_WithFake_PromoteAllClasses_GraduatesStudents()
        {
            var eligibleStudent = new Student { YearsCompleted = 3, IsGraduated = false };
            var ineligibleStudent = new Student { YearsCompleted = 2, IsGraduated = false };
            fakeBackingCollection.Add(eligibleStudent);
            fakeBackingCollection.Add(ineligibleStudent);

            serviceWithFake.PromoteAllClasses();

            Assert.True(eligibleStudent.IsGraduated);
            Assert.False(ineligibleStudent.IsGraduated);
        }
    }
}
